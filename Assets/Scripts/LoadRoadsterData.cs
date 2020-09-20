using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRoadsterData : MonoBehaviour
{
    public static LoadRoadsterData instance { get; set; }

    private TextAsset roadsterDataText;
    private string[] data;

    private List<RoadsterData> roadsterData;
    private double temp;
    
    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        if((roadsterDataText = Resources.Load<TextAsset>("Text/RoadsterData")) != null){
            data = roadsterDataText.text.Split(new char[]{'\n'});
            roadsterData = new List<RoadsterData>();
            ParseData();
        }
    }

    private void ParseData(){
        for(int i = 1; i < data.Length - 1; ++i){
            string[] row = data[i].Split(new char[]{','});
            
            RoadsterData r = new RoadsterData();
            r.ParseData(row);
            
            roadsterData.Add(r);
        }
    }

    public List<RoadsterData> GetRoadsterList(){
        return roadsterData;
    }

    public RoadsterData GetRoadsterRecord(int i){
        return roadsterData[i];
    }
}
