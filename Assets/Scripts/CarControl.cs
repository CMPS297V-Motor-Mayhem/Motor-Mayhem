using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CarControl : MonoBehaviour
{
    public long speed = 1500;
    public float maxRotationAngle = 15.0f;
    
    public List<Collider> throttleWheels;
    public List<Collider> steeringWheels;

    // helper variables:
    private float BoostcdTime = 0.0f;
    private float SheildcdTime = 0.0f;
    private float cd = 5.0f;
    
    private GameUIManager gameUIManager;
    private Abilities abilities;

    private void Start()
    {
        gameUIManager = GameObject.Find("UIManager").GetComponent<GameUIManager>();
        abilities = GetComponent<Abilities>();
    }

    private void FixedUpdate()
    {
        HandleInput();
    }

    // Helper Function:

    private void HandleInput()
    {
        HandleThrottleInput();
        HandleSteerInput();
        HandleBoostInput();
        HandleShieldInput();
    }

    private void HandleThrottleInput()
    {
        float dy = Input.GetAxis("Vertical");
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = dy * speed * Time.deltaTime;
        }
    }

    private void HandleSteerInput()
    {
        float dx = Input.GetAxis("Horizontal");
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = dx * maxRotationAngle;

            // rotate wheel models:
            wheel.transform.localEulerAngles = new Vector3(0, dx * maxRotationAngle, 0);
        }
    }

    private void HandleBoostInput()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && Time.time > BoostcdTime)
        {
            abilities.Boost();
            BoostcdTime = Time.time + cd;
            //StartCoroutine(gameUIManager.DisplayAbilityCooldown(gameUIManager.boostUIImage, 5, Ability.Boost));
            GameEvents.BoostEvent.Invoke(abilities.boostCooldown);
        }
    }

    private void HandleShieldInput()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Mouse1)) && Time.time > SheildcdTime)
        {
            StartCoroutine(abilities.Shield());
            SheildcdTime = Time.time + cd;
            //StartCoroutine(gameUIManager.DisplayAbilityCooldown(gameUIManager.shieldUIImage, 5, Ability.Shield));
            GameEvents.BoostEvent.Invoke(abilities.shieldDuration);
        }
    }
}