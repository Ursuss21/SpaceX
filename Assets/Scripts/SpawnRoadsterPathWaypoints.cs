using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RG.OrbitalElements;

public class SpawnRoadsterPathWaypoints : MonoBehaviour
{
    public static SpawnRoadsterPathWaypoints instance { get; set; }

    [SerializeField] private GameObject roadsterPath = null;
    private List<Transform> waypointsList;
    private int currentWaypoint;

    private Color rayColor = Color.white;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        waypointsList = new List<Transform>();
        currentWaypoint = 0;

        SpawnWaypoints();
    }

    private void SpawnWaypoints(){
        foreach(RoadsterData r in LoadRoadsterData.instance.GetRoadsterList()){
            waypointsList.Add(GenerateWaypoint(r).transform);
        }
    }

    private GameObject GenerateWaypoint(RoadsterData r){
        GameObject waypoint = new GameObject();
        SetWaypointPosition(waypoint, r);
        SetWaypointName(waypoint);
        SetWaypointParent(waypoint);
        NextWaypoint();
        return waypoint;
    }

    private void SetWaypointPosition(GameObject waypoint, RoadsterData r){
        Vector3Double vec = Calculations.CalculateOrbitalPosition(r.SemimajorAxis, r.Eccentricity, r.Inclination, r.LongitudeOfAscendingNode, r.PeriapsisArgument, r.TrueAnomaly);
        waypoint.transform.position = new Vector3((float)vec.x, (float)vec.y, (float)vec.z);
    }

    private void SetWaypointName(GameObject waypoint){
        waypoint.name = "Waypoint" + currentWaypoint;
    }

    private void SetWaypointParent(GameObject waypoint){
        waypoint.transform.parent = roadsterPath.transform;
    }

    private void NextWaypoint(){
        ++currentWaypoint;
    }

    public Transform GetWaypoint(int i){
        return waypointsList[i];
    }

    public int GetWaypointsCount(){
        return waypointsList.Count;
    }

    private void OnDrawGizmos() {
        for(int i = 0; i < waypointsList.Count; ++i){
            Vector3 position = waypointsList[i].position;
            if(i > 0){
                Vector3 previous = waypointsList[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 1000f);
            }
        }
    }
}
