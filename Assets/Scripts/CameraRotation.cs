using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject sun = null;

    private Vector3 previousPosition;

    private void Start(){
        cam.transform.LookAt(sun.transform);
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        if(Input.GetMouseButton(0)){
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = sun.transform.position;

            cam.transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f), direction.y * 180);
            cam.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), -direction.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0.0f, 0.0f, -100000.0f));
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
