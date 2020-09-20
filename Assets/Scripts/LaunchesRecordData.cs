using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchesRecordData : MonoBehaviour
{
    private int index;
    [SerializeField] private Text missionName = null;
    [SerializeField] private Text numberOfPayloads = null;
    [SerializeField] private Text rocketName = null;
    [SerializeField] private Text countryOfOrigin = null;
    [SerializeField] private Image isFutureLaunch = null;

    public void UpdateItemData(int idx){
        SetIndex(idx);
        SetMissionName();
        SetNumberOfPayloads();
        SetRocketName();
        SetCountryOfOrigin();
        SetFutureLaunchSprite();
    }

    private void SetIndex(int idx){
        index = idx;
    }

    public int GetIndex(){
        return index;
    }

    private void SetMissionName(){
        missionName.text = "Mission: " + LoadSpaceXLaunchesData.instance.GetLaunch(index).MissionName;
    }

    private void SetNumberOfPayloads(){
        numberOfPayloads.text = "Payloads number: " + LoadSpaceXLaunchesData.instance.GetLaunch(index).NumberOfPayloads.ToString();
    }

    private void SetRocketName(){
        rocketName.text = "Rocket: " + LoadSpaceXLaunchesData.instance.GetLaunch(index).RocketData.Name;
    }

    private void SetCountryOfOrigin(){
        countryOfOrigin.text = "Origin: " + LoadSpaceXLaunchesData.instance.GetLaunch(index).RocketData.Nationality;
    }
    
    private void SetFutureLaunchSprite(){
        if(LoadSpaceXLaunchesData.instance.GetLaunch(index).IsFutureLaunch){
            this.isFutureLaunch.sprite = SpritesLoader.instance.GetFutureSprite();
        }
        else{
            this.isFutureLaunch.sprite = SpritesLoader.instance.GetRocketSprite();
        }
    }
}
