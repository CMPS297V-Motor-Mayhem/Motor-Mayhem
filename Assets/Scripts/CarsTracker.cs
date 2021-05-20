using UnityEngine;

public class CarsTracker : MonoBehaviour
{
    public static GameObject[] CarsList;
    private GameUIManager gameUiManager;
    private bool triggeredOnce;

    private void Start()
    {
        triggeredOnce = false;
        gameUiManager = GameObject.Find("UIManager").GetComponent<GameUIManager>();
    }

    private void Update()
    {
        CarsList = GameObject.FindGameObjectsWithTag("Car");
        gameUiManager.carsRemaining = CarsList.Length - 1;

        if (CarsList.Length <= 1 && GameObject.Find("CarPlayer") != null && !triggeredOnce)
        {
            GameEvents.GameWinEvent.Invoke();
            triggeredOnce = true;
        }
    }
}