using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPerspective : Sense
{
    public float fieldOfView = 45;
    public float viewDistance = 100;

    // helper variables:
    [HideInInspector]
    public List<GameObject> spottedCars;
    public GameObject ClosestCar { get {return this.closestCar; } }
    private GameObject closestCar;

    protected override void Initialize()
    {
        // initialize variables:
        spottedCars = new List<GameObject>();
        closestCar = null;
    }

    protected override void LateUpdateSense()
    {
        SpotCars();
        DetermineClosestCar();
    }

    private void SpotCars()
    {
        // clear list first:
        this.spottedCars.Clear();

        Vector3 origin = transform.position;

        foreach (var car in CarsTracker.CarsList)
        {
            Vector3 direction = car.transform.position - transform.position;
            Ray ray = new Ray(origin, direction);
            RaycastHit hit;


            if (this.enableDebug)
                Debug.DrawRay(origin, direction * viewDistance, Color.blue);

            if (Vector3.Angle(transform.right, direction) < fieldOfView)
            {
                if (Physics.Raycast(ray, out hit, viewDistance))
                {
                    // car in sight:
                    spottedCars.Add(car);
                }
            }
        }
    }

    private void DetermineClosestCar()
    {
        if (this.spottedCars.Count == 0)
            return;

        // local variables:
        float minDistance = Mathf.Infinity;
        GameObject closestCar = null;

        foreach (GameObject car in spottedCars)
        {
            float distance = Vector3.Distance(transform.position, car.transform.position);
            if (distance < minDistance)
            {
                closestCar = car;
            }
        }

        // raise event if the closest car changed:
        if (this.closestCar != closestCar)
        {
            GameEvents.DeterminedClosestCarEvent.Invoke(closestCar);
        }

        // after finding closest car locally, assign it globally:
        this.closestCar = closestCar;
    }

    private void OnDrawGizmos()
    {
        if (enableDebug)
        {
            // front ray:
            Vector3 frontRayPoint = transform.position + transform.forward * viewDistance;

            // left ray:
            Vector3 leftRayPoint = frontRayPoint;
            leftRayPoint.x += fieldOfView / 2;

            // right ray:
            Vector3 rightRayPoint = frontRayPoint;
            rightRayPoint.x -= fieldOfView / 2;

            // draw rays:
            Debug.DrawLine(transform.position, frontRayPoint, Color.green);
            Debug.DrawLine(transform.position, leftRayPoint, Color.green);
            Debug.DrawLine(transform.position, rightRayPoint, Color.green);
        }
    }
}
