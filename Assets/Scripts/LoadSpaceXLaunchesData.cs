using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class LoadSpaceXLaunchesData : MonoBehaviour
{
    public static LoadSpaceXLaunchesData instance { get; set; }

    private readonly string spaceXLaunchesURL = "https://api.spacexdata.com/v3/launches";

    private UnityWebRequest spaceXLaunchesRequest;
    private JSONNode spaceXLaunchesInfo;

    private List<LaunchData> launchesData;
    private string tempdata;

    private LaunchData launchInfo;
    private RocketData rocketInfo;
    private ShipData shipInfo;
    
    [SerializeField] private GameObject loadingScreen = null;
    [SerializeField] private Slider slider = null;
    private float progress;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        launchesData = new List<LaunchData>();
    }

    private void Start(){
        progress = 0.0f;
        StartCoroutine(UpdateLoadingScreen());
        StartCoroutine(LoadData());
    }

    private IEnumerator LoadData(){
        spaceXLaunchesRequest = UnityWebRequest.Get(spaceXLaunchesURL);
        yield return spaceXLaunchesRequest.SendWebRequest();

        if(spaceXLaunchesRequest.isNetworkError || spaceXLaunchesRequest.isHttpError){
            Debug.LogError(spaceXLaunchesRequest.error);
            yield break;
        }

        UpdateProgress(0.4f);
        ParseJSON();

        yield return StartCoroutine(LoadSpaceXRocketsData.instance.LoadSpaceXRockets());
        yield return StartCoroutine(LoadSpaceXShipsData.instance.LoadSpaceXShips());
        LoadLaunchesDataToObjects();
        LaunchesUI.instance.SpawnListRecords();
        loadingScreen.SetActive(false);
    }

    private void ParseJSON(){
        spaceXLaunchesInfo = JSON.Parse(spaceXLaunchesRequest.downloadHandler.text);
        UpdateProgress(0.2f);
    }

    private void LoadLaunchesDataToObjects(){
        foreach(JSONNode x in spaceXLaunchesInfo){
            CreateLaunchDataObject();
            UpdateLaunchData(x);
            launchesData.Add(launchInfo);
        }
        UpdateProgress(0.1f);
    }

    private void CreateLaunchDataObject(){
        launchInfo = new LaunchData();
    }

    private void UpdateLaunchData(JSONNode x){
        SetLaunchName(x);
        SetLaunchNumberOfPayloads(x);
        SetLaunchRocketInfo(x);
        SetLaunchShipInfo(x);
        SetFutureLaunchStatus(x);
    }

    private void SetLaunchName(JSONNode x){
        launchInfo.MissionName = x["mission_name"];
    }

    private void SetLaunchNumberOfPayloads(JSONNode x){
        launchInfo.NumberOfPayloads = x["rocket"]["second_stage"]["payloads"].Count;
    }

    private void SetLaunchRocketInfo(JSONNode x){
        rocketInfo = LoadSpaceXRocketsData.instance.GetRocketsData().Find(y => y.Id == x["rocket"]["rocket_id"]);
        if(rocketInfo != null){
            launchInfo.RocketData = rocketInfo;
        }
    }
    
    private void SetLaunchShipInfo(JSONNode x){
        launchInfo.ShipData = new List<ShipData>();
        for(int i = 0; i < x["ships"].Count; ++i){
            shipInfo = LoadSpaceXShipsData.instance.GetShipsData().Find(y => y.Id == x["ships"][i]);
            if(shipInfo != null){
                launchInfo.ShipData.Add(shipInfo);
            }
        }
    }

    private void SetFutureLaunchStatus(JSONNode x){
        DateTime time;
        tempdata = x["launch_date_utc"];
        DateTime.TryParse(tempdata, out time);
        if(DateTime.Compare(time, DateTime.UtcNow) > 0){
            launchInfo.IsFutureLaunch = true;
        }
        else{
            launchInfo.IsFutureLaunch = false;
        }
    }

    IEnumerator UpdateLoadingScreen(){
        loadingScreen.SetActive(true);
        while(progress != 1.0f){
            slider.value = progress;
            yield return null;
        }
    }

    public void UpdateProgress(float value){
        progress += value;
    }

    public List<LaunchData> GetLaunchesData(){
        return launchesData;
    }

    public LaunchData GetLaunch(int i){
        return launchesData[i];
    }

    public int GetLaunchesDataSize(){
        return launchesData.Count;
    }
}
