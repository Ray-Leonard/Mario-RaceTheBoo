using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Reference: https://www.youtube.com/watch?v=LbDQHv9z-F0
 Filmstorm - Free 3rd Person Camera Setup & Camera Collision Tutorial
 */

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;

    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            
            distance = Mathf.Clamp(hit.distance * .6f, minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
