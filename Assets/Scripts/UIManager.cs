using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Menus Related Settings")]
    public Canvas mainMenu;
    public Canvas pickCar;
    public Canvas about;

    [Header("Cars Related Settings")]
    public List<GameObject> cars;
    public Transform spawningTransform;

    // helper variables:
    private int carIndex;
    private Animator cameraFsm;
    private GameObject spawnedCar;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        carIndex = 0;
        cameraFsm = Camera.main.GetComponent<Animator>();
    }

    public void OnStartClick()
    {
        // set camera animation:
        cameraFsm.SetBool("PickingCar", true);

        // hide main menu:
        mainMenu.gameObject.SetActive(false);

        // display pick car menu:
        pickCar.gameObject.SetActive(true);

        // spawn first car:
        SpawnCar();
    }

    public void OnAboutClick()
    {
        // hide main menu:
        mainMenu.gameObject.SetActive(false);

        // display about menu:
        about.gameObject.SetActive(true);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnPreviousCarClick()
    {
        // update index:
        if (carIndex == 0)
            carIndex = this.cars.Count - 1;
        else
            carIndex--;

        // spawn car:
        SpawnCar();
    }

    public void OnNextCarClick()
    {
        // update index:
        carIndex = (carIndex + 1) % this.cars.Count;

        // spawn car:
        SpawnCar();
    }

    public void OnKickOffClick()
    {
        // save car index in PlayerPrefs
        PlayerPrefs.SetInt("CarIndex", this.carIndex);

        // load main scene:
        SceneManager.LoadScene("SampleScene");
    }

    private void SpawnCar()
    {
        // find target car:
        GameObject targetCar = this.cars[carIndex];

        // delete existing spawned car:
        if (this.spawnedCar != null)
        {
            Destroy(this.spawnedCar);
        }

        // spawn a new car:
        this.spawnedCar = Instantiate(targetCar);

        // position and rotate it properly:
        this.spawnedCar.transform.position = spawningTransform.position;
        this.spawnedCar.transform.rotation = spawningTransform.rotation;

        // adjust Y position of spawned car:
        adjustYPosition();
    }

    private void adjustYPosition()
    {
        // get car height:
        Bounds carBounds = this.GetTotalBounds(this.spawnedCar);
        float carHeight = carBounds.extents.y;

        // find new position after adjustement:
        Vector3 adjustedPosition = this.spawnedCar.transform.position;
        adjustedPosition.y = carHeight;

        // update position:
        this.spawnedCar.transform.position = adjustedPosition;
    }

    private Bounds GetTotalBounds(GameObject obj)
    {
        Bounds totalBounds = new Bounds();
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer != null)
        {
            totalBounds.Encapsulate(renderer.bounds);
        }

        if (obj.transform.childCount > 0)
        {
            // when object has children:
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                // get child:
                Transform child = obj.transform.GetChild(i);

                // recursive call:
                Bounds childBounds = GetTotalBounds(child.gameObject);

                // encapsulate bounds:
                totalBounds.Encapsulate(childBounds);
            }
        }

        return totalBounds;
    }
}
