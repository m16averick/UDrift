using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;
    
    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;
    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    private void FixedUpdate() {
        //Get forward/revers acceleration from the vertical axis (W and S keys)
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        //If we're pressing space, give currentBreakingForce a value
        if (Input.GetKey(KeyCode.Space)) 
            currentBreakForce = breakingForce;
        else 
            currentBreakForce = 0f;

        //Apply acceleration to back wheels 
        backRight.motorTorque = currentAcceleration;
        backLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;

        //Take care of steering 
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
        
        //Update wheel meshes
        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);
    } 

    void UpdateWheel (WheelCollider col, Transform trans) {
        //Get wheel collider state
        Vector3 position; 
        Quaternion rotation; 
        col.GetWorldPose(out position, out rotation);

        // //Set wheel transform state
        trans.position = position;
        trans.rotation = rotation;


    }
}
