using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsSpawner : MonoBehaviour
{
    // settings:
    [Header("AI Settings")]
    public int numberOfAICars = 10;
    public RuntimeAnimatorController aiCarAnimatorController;

    [Header("Spawning Settings")]
    public float distanceFromCenterToSpawn = 3.0f;
    public Transform platformCenter;
    public bool spawnRandomly = false;

    // helper variables:
    private List<GameObject> carsList;
    private int carIndex;

    void Start()
    {
        Initialize();
        SpawnCars();
    }

    private void Initialize()
    {
        // initialize variables:
        carsList = CarsList.Instance.carsList;
        carIndex = PlayerPrefs.GetInt("CarIndex");
    }

    private void SpawnCars()
    {
        // get all cars' positions:
        int totalNumberOfCars = this.numberOfAICars + 1; // + 1 for the player himself
        List<Vector3> carsPositions = ComputeCarsPositions(totalNumberOfCars, this.distanceFromCenterToSpawn);

        // spawn player:
        Vector3 playerCarPosition = carsPositions[0];
        SpawnPlayerCar(playerCarPosition);

        // spawn AI cars:
        List<Vector3> aiCarsPositions = carsPositions.GetRange(1, totalNumberOfCars - 1);
        SpawnAICars(aiCarsPositions);
    }

    private List<Vector3> ComputeCarsPositions(int numberOfCars, float distanceFromCenter)
    {
        List<Vector3> result = new List<Vector3>();

        if (!spawnRandomly)
        {
            // distribute the positions equally around the circle:
            for (int i = 0; i < numberOfCars; i++)
            {
                /*
                 * x = r * cos (alpha)
                 * z = r * sin (alpha)
                 * 
                 * where :
                 * r: is the radius of the circle
                 * alpha: is the angle between each car
                 */

                float r = this.distanceFromCenterToSpawn;
                float alpha = 2 * Mathf.PI / numberOfCars;

                float x = r * Mathf.Cos(alpha * i);
                float z = r * Mathf.Sin(alpha * i);

                // compute vector3 position:
                Vector3 pos = this.platformCenter.position;
                pos.x += x;
                pos.z += z;

                // add position to the list:
                result.Add(pos);
            }
        }
        else
        {
            for (int i = 0; i < numberOfCars; i++)
            {
                // compute position:
                Vector3 pos = this.platformCenter.position;
                pos.x += Random.Range(0.0f, this.distanceFromCenterToSpawn);
                pos.z += Random.Range(0.0f, this.distanceFromCenterToSpawn);

                // add position to the list:
                result.Add(pos);
            }
        }

        return result;
    }

    private void SpawnPlayerCar(Vector3 pos)
    {
        // use the picked car from the menu to spawn:
        GameObject targetCar = this.carsList[this.carIndex];

        // spawn player car:
        GameObject spawnedPlayerCar = Instantiate(targetCar);

        // set player car's position:
        spawnedPlayerCar.transform.position = pos;
        spawnedPlayerCar.transform.LookAt(this.platformCenter);

        // adjust Y position:
        float platformHeight = this.platformCenter.position.y;
        Helpers.AdjustYPosition(spawnedPlayerCar, platformHeight);

        // attach player components:
        AttachPlayerCarComponents(spawnedPlayerCar);
    }

    private void SpawnAICars(List<Vector3> positions)
    {
        foreach (Vector3 pos in positions)
        {
            // choose random car from the list:
            int index = Random.Range(0, carsList.Count);

            // spawn car from the list:
            GameObject targetCar = this.carsList[index];
            GameObject spawnedAiCar = Instantiate(targetCar);

            // set AI car's position:
            spawnedAiCar.transform.position = pos;
            spawnedAiCar.transform.LookAt(this.platformCenter);

            // adjust Y position:
            float platformHeight = this.platformCenter.position.y;
            Helpers.AdjustYPosition(spawnedAiCar, platformHeight);

            // attach AI components:
            AttachAICarComponents(spawnedAiCar);
        }
    }

    private void AttachPlayerCarComponents(GameObject playerCar)
    {
        // add scripts:
        playerCar.AddComponent<CarControl>();
    }

    private void AttachAICarComponents(GameObject aiCar)
    {
        // add scripts:
        aiCar.AddComponent<AIBehaviors>();
        aiCar.AddComponent<AIPerspective>();

        // add FSM:
        Animator fsm = aiCar.AddComponent<Animator>();
        fsm.runtimeAnimatorController = this.aiCarAnimatorController;
    }
}
