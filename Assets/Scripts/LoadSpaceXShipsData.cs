using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class LoadSpaceXShipsData : MonoBehaviour
{
    public static LoadSpaceXShipsData instance { get; set; }
    
    private readonly string spaceXShipsURL = "https://api.spacexdata.com/v3/ships";

    private UnityWebRequest spaceXShipsRequest;
    private JSONNode spaceXShipsInfo;

    private List<ShipData> shipsData;
    private ShipData shipInfo;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        shipsData = new List<ShipData>();
    }

    public IEnumerator LoadSpaceXShips(){
        spaceXShipsRequest = UnityWebRequest.Get(spaceXShipsURL);
        yield return spaceXShipsRequest.SendWebRequest();

        if(spaceXShipsRequest.isNetworkError || spaceXShipsRequest.isHttpError){
            Debug.LogError(spaceXShipsRequest.error);
            yield break;
        }

        ParseJSON();
        LoadSpaceXLaunchesData.instance.UpdateProgress(0.1f);
        LoadShipsDataToObjects();
    }

    private void ParseJSON(){
        spaceXShipsInfo = JSON.Parse(spaceXShipsRequest.downloadHandler.text);
    }

    private void LoadShipsDataToObjects(){
        foreach(JSONNode x in spaceXShipsInfo){
            CreateShipDataObject();
            UpdateShipData(x);
            shipsData.Add(shipInfo);
        }
    }

    private void UpdateShipData(JSONNode x){
        SetShipId(x);
        SetShipName(x);
        SetShipNumberOfMissions(x);
        SetShipType(x);
        SetShipHomePort(x);
        SetShipImageURL(x);
    }

    private void CreateShipDataObject(){
        shipInfo = new ShipData();
    }

    private void SetShipId(JSONNode x){
        shipInfo.Id = x["ship_id"];
    }

    private void SetShipName(JSONNode x){
        shipInfo.Name = x["ship_name"];
    }

    private void SetShipNumberOfMissions(JSONNode x){
        shipInfo.NumberOfMissions = x["missions"].Count;
    }

    private void SetShipType(JSONNode x){
        shipInfo.ShipType = x["ship_type"];
    }

    private void SetShipHomePort(JSONNode x){
        shipInfo.HomePort = x["home_port"];
    }

    private void SetShipImageURL(JSONNode x){
        shipInfo.ImageURL = x["image"];
    }

    public List<ShipData> GetShipsData(){
        return shipsData;
    }
}
