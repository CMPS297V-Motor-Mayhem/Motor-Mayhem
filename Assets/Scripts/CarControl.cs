using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CarControl : MonoBehaviour
{
    public float speed = 1500.0f;
    public float maxRotationAngle = 15.0f;

    public List<Collider> throttleWheels;
    public List<Collider> steeringWheels;
    private bool canSheild;
    private bool canBoost;
    private float cdTimeBoost;
    private float cdTimeSheild;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) && canBoost)
        {
            Debug.Log("Boost");

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Mouse1) && canSheild)
        {
            Debug.Log("Sheild");
        }
    }


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