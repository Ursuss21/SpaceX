using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadsterUI : MonoBehaviour
{
    public static RoadsterUI instance { get; set; }

    [SerializeField] private GameObject epochText = null;
    [SerializeField] private GameObject dateText = null;
    [SerializeField] private GameObject semimajorAxisText = null;
    [SerializeField] private GameObject eccentricityText = null;
    [SerializeField] private GameObject inclinationText = null;
    [SerializeField] private GameObject longitudeOfAscendingNodeText = null;
    [SerializeField] private GameObject periapsisArgumentText = null;
    [SerializeField] private GameObject meanAnomalyText = null;
    [SerializeField] private GameObject trueAnomalyText = null;

    private int currentWaypoint;

    private string tempdata;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        GetCurrentWaypointID();
        UpdateTexts();
    }

    private void UpdateTexts(){
        UpdateEpochText();
        UpdateDateText();
        UpdateSemimajorAxisText();
        UpdateEccentricityText();
        UpdateInclinationText();
        UpdateLongitudeOfAscendingNodeText();
        UpdatePeriapsisArgumentText();
        UpdateMeanAnomalyText();
        UpdateTrueAnomalyText();
    }

    private void GetCurrentWaypointID(){
        currentWaypoint = RoadsterMovement.instance.GetCurrentWaypointID();
    }

    private void UpdateEpochText(){
        tempdata = LoadRoadsterData.instance.GetRoadsterRecord(currentWaypoint).Epoch.ToString();
        epochText.GetComponent<Text>().text = "Epoch: " + tempdata;
    }

    private void UpdateDateText(){
        DateTime time;
        tempdata = LoadRoadsterData.instance.GetRoadsterRecord(currentWaypoint).Date;
        DateTime.TryParse(tempdata, out time);
        dateText.GetComponent<Text>().text = "Date: " + time.ToLocalTime();
    }

    private void UpdateSemimajorAxisText(){
        tempdata = LoadRoadsterData.instance.GetRoadsterRecord(currentWaypoint).SemimajorAxis.ToString();
        semimajorAxisText.GetComponent<Text>().text = "Semi-major axis: " + tempdata;
    }

    private void UpdateEccentricityText(){
        tempdata = LoadRoadsterData.instance.GetRoadsterRecord(currentWaypoint).Eccentricity.ToString();
        eccentricityText.GetComponent<Text>().text = "Eccentricity: " + tempdata;
    }

    private void UpdateInclinationText(){
        tempdata = LoadRoadsterData.instance.GetRoadsterRecord(currentWaypoint).Inclination.ToString();
        inclinationText.GetComponent<Text>().text = "Inclination: " + tempdata;
    }

    private void UpdateLongitudeOfAscendingNodeText(){
        tempdata = LoadRoadsterData.instance.GetRoadsterRecord(currentWaypoint).LongitudeOfAscendingNode.ToString();
        longitudeOfAscendingNodeText.GetComponent<Text>().text = "Longitude of ascending node: " + tempdata;
    }

    private void UpdatePeriapsisArgumentText(){
        tempdata = LoadRoadsterData.instance.GetRoadsterRecord(currentWaypoint).PeriapsisArgument.ToString();
        periapsisArgumentText.GetComponent<Text>().text = "Argument of periapsis: " + tempdata;
    }

    private void UpdateMeanAnomalyText(){
        tempdata = LoadRoadsterData.instance.GetRoadsterRecord(currentWaypoint).MeanAnomaly.ToString();
        meanAnomalyText.GetComponent<Text>().text = "Mean Anomaly: " + tempdata;
    }

    private void UpdateTrueAnomalyText(){
        tempdata = LoadRoadsterData.instance.GetRoadsterRecord(currentWaypoint).TrueAnomaly.ToString();
        trueAnomalyText.GetComponent<Text>().text = "True Anomaly: " + tempdata;
    }
}
