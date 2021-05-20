using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    // settings:
    public float scoreCountingSpeedRatio = 2.0f;

    // singleton:
    public static ScoreCounter Instance { get; private set; }

    // helper variables:
    public int Score { get { return (int)this.score; } }
    private bool keepCounting = true;
    private float score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // add event listener:
        GameEvents.GameLoseEvent.AddListener(HandleGameLostEvent);

        // use coroutine to count score:
        StartCoroutine(CountScore());
    }

    // Event Handler:

    void HandleGameLostEvent()
    {
        keepCounting = false;
    }

    // Coroutine:

    IEnumerator CountScore()
    {
        while (keepCounting)
        {
            score += Time.deltaTime * scoreCountingSpeedRatio;
            yield return new WaitForEndOfFrame();
        }
    }
}
