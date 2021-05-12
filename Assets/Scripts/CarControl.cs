using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CarControl : MonoBehaviour
{
    public float speed = 2000.0f;
    public float maxRotationAngle = 30.0f;

    public List<Collider> throttleWheels;
    public List<Collider> steeringWheels;

    private void FixedUpdate()
    {
        Throttle();
        Steer();
    }

    private void Throttle()
    {
        float dy = Input.GetAxis("Vertical");
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = dy * speed * Time.deltaTime;
        }
    }

    private void Steer()
    {
        float dx = Input.GetAxis("Horizontal");
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = dx * maxRotationAngle;

            // rotate wheel models:
            wheel.transform.localEulerAngles = new Vector3(0, dx * maxRotationAngle, 0);
        }
    }
}