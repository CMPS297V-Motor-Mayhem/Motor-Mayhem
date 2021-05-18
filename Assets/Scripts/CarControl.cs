using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CarControl : MonoBehaviour
{
    public long speed = 1500;
    public float maxRotationAngle = 15.0f;
    GameUIManager gameUIManager;

    public List<Collider> throttleWheels;
    public List<Collider> steeringWheels;
    private float BoostcdTime = 0.0f;
    private float SheildcdTime = 0.0f;
    private float cd = 5.0f;

    private void Start()
    {
        gameUIManager = GameObject.Find("UIManager").GetComponent<GameUIManager>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && Time.time > BoostcdTime)
        {
            GetComponent<Abilities>().Boost();
            BoostcdTime = Time.time + cd;
            StartCoroutine(gameUIManager.DisplayAbilityCooldown(gameUIManager.boostUIImage, 5, Ability.Boost));
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Mouse1)) && Time.time > SheildcdTime)
        {
            StartCoroutine(GetComponent<Abilities>().Shield());
            SheildcdTime = Time.time + cd;
            StartCoroutine(gameUIManager.DisplayAbilityCooldown(gameUIManager.shieldUIImage, 5, Ability.Shield));
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