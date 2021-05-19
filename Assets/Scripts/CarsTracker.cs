using UnityEngine;

public class CarsTracker : MonoBehaviour
{
    public GameObject[] CarsList;
    private GameUIManager gameUiManager;

    private void Start()
    {
        gameUiManager = GameObject.Find("UIManager").GetComponent<GameUIManager>();
    }

    private void Update()
    {
        CarsList = GameObject.FindGameObjectsWithTag("Car");
        gameUiManager.carsRemaining = CarsList.Length - 1;

        if (CarsList.Length <= 1)
        {
            GameEvents.GameWinEvent.Invoke();
        }
    }
}