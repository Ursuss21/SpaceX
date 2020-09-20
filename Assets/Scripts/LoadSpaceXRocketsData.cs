using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class LoadSpaceXRocketsData : MonoBehaviour
{
    public static LoadSpaceXRocketsData instance { get; set; }
    
    private readonly string spaceXRocketsURL = "https://api.spacexdata.com/v3/rockets";

    private UnityWebRequest spaceXRocketsRequest;
    private JSONNode spaceXRocketsInfo;

    private List<RocketData> rocketsData;

    private RocketData rocketInfo;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        rocketsData = new List<RocketData>();
    }

    public IEnumerator LoadSpaceXRockets(){
        spaceXRocketsRequest = UnityWebRequest.Get(spaceXRocketsURL);
        yield return spaceXRocketsRequest.SendWebRequest();

        if(spaceXRocketsRequest.isNetworkError || spaceXRocketsRequest.isHttpError){
            Debug.LogError(spaceXRocketsRequest.error);
            yield break;
        }

        ParseJSON();
        LoadSpaceXLaunchesData.instance.UpdateProgress(0.1f);
        LoadRocketsDataToObjects();
    }

    private void ParseJSON(){
        spaceXRocketsInfo = JSON.Parse(spaceXRocketsRequest.downloadHandler.text);
    }

    private void LoadRocketsDataToObjects(){
        foreach(JSONNode x in spaceXRocketsInfo){
            CreateRocketDataObject();
            UpdateRocketData(x);
            rocketsData.Add(rocketInfo);
        }
    }

    private void UpdateRocketData(JSONNode x){
        SetRocketId(x);
        SetRocketName(x);
        SetRocketNationality(x);
    }

    private void CreateRocketDataObject(){
        rocketInfo = new RocketData();
    }

    private void SetRocketId(JSONNode x){
        rocketInfo.Id = x["rocket_id"];
    }

    private void SetRocketName(JSONNode x){
        rocketInfo.Name = x["rocket_name"];
    }

    private void SetRocketNationality(JSONNode x){
        rocketInfo.Nationality = x["country"];
    }

    public List<RocketData> GetRocketsData(){
        return rocketsData;
    }
}
