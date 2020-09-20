using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipsRecordData : MonoBehaviour
{
    private int index;
    [SerializeField] private Text shipName = null;
    [SerializeField] private Text shipType = null;
    [SerializeField] private Text homePort = null;
    [SerializeField] private Text numberOfMissions = null;

    public void UpdateItemData(int idx){
        SetIndex(idx);
        SetShipName();
        SetShipType();
        SetHomePort();
        SetNumberOfMissions();
    }

    private void SetIndex(int idx){
        index = idx;
    }

    private void SetShipName(){
        shipName.text = "Ship name: " + ShipsUI.instance.GetShipData(index).Name;
    }

    private void SetShipType(){
        shipType.text = "Ship type: " + ShipsUI.instance.GetShipData(index).ShipType;
    }

    private void SetHomePort(){
        homePort.text = "Home port: " + ShipsUI.instance.GetShipData(index).HomePort;
    }

    private void SetNumberOfMissions(){
        numberOfMissions.text = "Number of missions: " + ShipsUI.instance.GetShipData(index).NumberOfMissions.ToString();
    }
}
