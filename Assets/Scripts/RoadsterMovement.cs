using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsterMovement : MonoBehaviour
{

    public static RoadsterMovement instance { get; set; }

    private int currentWaypointID;
    [SerializeField] private float speed;
    private float reachDistance;
    private float rotationSpeed;
    private float distance;
    private Quaternion rotation;

    private Vector3 lastPosition;
    private Vector3 currentPosition;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        reachDistance = 1.0f;
        rotationSpeed = 5.0f;
        speed = 7500.0f;
        lastPosition = transform.position;
        this.transform.position = SpawnRoadsterPathWaypoints.instance.GetWaypoint(0).position;
    }

    void Update()
    {
        UpdateRoadsterPosition();
        //UpdateRoadsterRotation();

        CheckIfReachedCurrentWaypoint(distance);
        DrawRoadsterTrail.instance.DrawTrail();

        CheckIfReachedEndOfSimulation();
    }

    private void UpdateRoadsterPosition(){
        distance = Vector3.Distance(SpawnRoadsterPathWaypoints.instance.GetWaypoint(currentWaypointID).position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, SpawnRoadsterPathWaypoints.instance.GetWaypoint(currentWaypointID).position, Time.deltaTime * speed);
    }

    private void UpdateRoadsterRotation(){
        rotation = Quaternion.LookRotation(SpawnRoadsterPathWaypoints.instance.GetWaypoint(currentWaypointID).position, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void CheckIfReachedCurrentWaypoint(float distance){
        if(distance <= reachDistance){
            ++currentWaypointID;
        }
    }

    private void CheckIfReachedEndOfSimulation(){
        if(currentWaypointID >= SpawnRoadsterPathWaypoints.instance.GetWaypointsCount()){
            currentWaypointID = 0;
        }
    }

    public int GetCurrentWaypointID(){
        return currentWaypointID;
    }
}
