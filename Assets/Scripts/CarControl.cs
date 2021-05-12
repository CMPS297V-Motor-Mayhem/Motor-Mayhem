using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CarControl : MonoBehaviour
{
    public float speed = 2000.0f;
    public float maxRotationAngle = 30.0f;
    public float bouncinessForce = 2;

    public List<Collider> throttleWheels;
    public List<Collider> steeringWheels;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
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

    private void OnCollisionEnter(Collision collision)
    {
        // simulate the bouciness effect:
        if (!collision.gameObject.CompareTag("Ground"))
        {
            float impactForce = collision.impulse.magnitude;
            Vector3 normal = collision.contacts[0].normal;
            rb.AddForce(normal * impactForce * this.bouncinessForce);
        }
    }
}
