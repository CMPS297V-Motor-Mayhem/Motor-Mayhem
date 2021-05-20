using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Menus Related Settings")]
    public Canvas mainMenu;
    public Canvas pickCarMenu;
    public Canvas aboutMenu;

    [Header("Cars Related Settings")]
    public Transform spawningTransform;

    // helper variables:
    private int carIndex;
    private Animator cameraFsm;
    private GameObject spawnedCar;
    private List<GameObject> carsList;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        carIndex = 0;
        cameraFsm = Camera.main.GetComponent<Animator>();
        carsList = CarsList.Instance.carsList;
    }

    // click events:

    public void OnStartClick()
    {
        // set camera animation parameter:
        cameraFsm.SetBool("PickingCar", true);

        // hide main menu:
        mainMenu.gameObject.SetActive(false);

        // display pick car menu:
        pickCarMenu.gameObject.SetActive(true);

        // spawn first car in the list:
        SpawnCar();
    }

    public void OnAboutClick()
    {
        // hide main menu:
        mainMenu.gameObject.SetActive(false);

        // display about menu:
        aboutMenu.gameObject.SetActive(true);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnPreviousCarClick()
    {
        // update index:
        if (carIndex == 0)
            carIndex = this.carsList.Count - 1;
        else
            carIndex--;

        // spawn car:
        SpawnCar();
    }

    public void OnNextCarClick()
    {
        // update index:
        carIndex = (carIndex + 1) % this.carsList.Count;

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

    public void OnBackClick()
    {
        // set camera animation parameter:
        cameraFsm.SetBool("PickingCar", false);

        // hide pick car menu
        if (pickCarMenu.gameObject.activeSelf)
            pickCarMenu.gameObject.SetActive(false);

        // hide about menu
        if (aboutMenu.gameObject.activeSelf)
            aboutMenu.gameObject.SetActive(false);

        // display main menu:
        mainMenu.gameObject.SetActive(true);
    }

    // helper functions:

    private void SpawnCar()
    {
        // find target car from the list:
        GameObject targetCar = this.carsList[carIndex];

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
        Helpers.AdjustYPosition(this.spawnedCar, 0.05f);
    }
}
