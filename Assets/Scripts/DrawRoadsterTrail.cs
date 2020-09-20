using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRoadsterTrail : MonoBehaviour
{
    public static DrawRoadsterTrail instance { get; set; }

    private LineRenderer lineRenderer;
    [SerializeField] private GameObject roadster = null;
    private Color startColor;
    private Color endColor;
    
    private float startWidth;
    private float endWidth;

    private bool useWorldSpace;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        lineRenderer = roadster.GetComponent<LineRenderer>();

        startColor = Color.white;
        endColor = Color.black;

        startWidth = 1000.0f;
        endWidth = 1000.0f;

        useWorldSpace = true;
    }

    public void DrawTrail(){
        SetTrailStartColor(startColor);
        SetTrailEndColor(endColor);
        SetTrailStartWidth(startWidth);
        SetTrailEndWidth(endWidth);
        UseWorldSpace(useWorldSpace);
        SetTrailRootPosition();
        GenerateTrailTail();
    }

    private void SetTrailStartColor(Color color){
        lineRenderer.startColor = color;
    }

    private void SetTrailEndColor(Color color){
        lineRenderer.endColor = color;
    }

    private void SetTrailStartWidth(float width){
        lineRenderer.startWidth = width;
    }

    private void SetTrailEndWidth(float width){
        lineRenderer.endWidth = width;
    }

    private void UseWorldSpace(bool x){
        lineRenderer.useWorldSpace = x;
    }

    private void SetTrailRootPosition(){
        lineRenderer.SetPosition(0, roadster.transform.position);
    }

    private void GenerateTrailTail(){
        for(int i = RoadsterMovement.instance.GetCurrentWaypointID() - 1, j = 1; i >= 0; --i, ++j){
            if(i > RoadsterMovement.instance.GetCurrentWaypointID() - 20){
                SetTrailLength(j + 1);
                lineRenderer.SetPosition(j, SpawnRoadsterPathWaypoints.instance.GetWaypoint(i).position);
            }
            else{
                if(RoadsterMovement.instance.GetCurrentWaypointID() > 606){
                    SetTrailLength(5);
                }
                else{
                    SetTrailLength(20);
                }
                break;
            }
        }
    }

    private void SetTrailLength(int x){
        lineRenderer.positionCount = x;
    }
}
