using UnityEngine;
using System.Collections;

public class FrogMovement : MonoBehaviour {
    
    public float jumpElevationInDegrees = 45;
    public float jumpSpeedInMPS = 5;
    public float maxDistance = 2;
    public float jumpSpeedTolerance = 5;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // transform.position - this location, direction (2nd param) - down (down nera todel priesingas nei up), maxDistance - 3rd param
        bool isOnGround = Physics.Raycast(transform.position, -transform.up, maxDistance);
        var speed = GetComponent<Rigidbody>().velocity.magnitude;   //kokiu greiciu juda objektas
        bool isNearStationary = speed < jumpSpeedTolerance; // ar juda greiciau nei galimas tolerance
        if (GvrViewer.Instance.Triggered && isOnGround && isNearStationary) {
            var camera = GetComponentInChildren<Camera>();  //kameros vektorius (ten kur ziurima)
            var projectedLookDirection = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up);   //kameros vektoriaus projekcija i x asi
            var radiansToRotate = Mathf.Deg2Rad * jumpElevationInDegrees;
            var unnormalizedJumpDirection = Vector3.RotateTowards(projectedLookDirection, Vector3.up, radiansToRotate, 0);
            var jumpVector = unnormalizedJumpDirection.normalized * jumpSpeedInMPS;
            GetComponent<Rigidbody>().AddForce(jumpVector, ForceMode.VelocityChange);
        }
	}
}
