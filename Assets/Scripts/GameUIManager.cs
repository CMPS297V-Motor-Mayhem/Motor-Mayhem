using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Canvas gameWinMenu;
    public Canvas gameLoseMenu;
    public Canvas pauseMenu;
    public Image boostUIImage;
    public Image shieldUIImage;

    private int score;

    private void Start()
    {
        // add listeners to events:
        GameEvents.GameWinEvent.AddListener(HandleGameWinEvent);
        GameEvents.GameLoseEvent.AddListener(HandleGameLoseEvent);
        GameEvents.PauseEvent.AddListener(HandlePauseEvent);
        GameEvents.BoostEvent.AddListener(HandleBoostEvent);
        GameEvents.ShieldEvent.AddListener(HandleShieldEvent);
    }

    // Game Events Handlers:

    private void HandleGameWinEvent(int score)
    {
        this.score = score;

        // display game win menu:
        this.gameWinMenu.gameObject.SetActive(true);
    }

    private void HandleGameLoseEvent(int score)
    {
        this.score = score;

        // display game lose menu:
        this.gameLoseMenu.gameObject.SetActive(true);
    }

    private void HandlePauseEvent()
    {
        // pause game:
        this.PauseGame();

        // display pause menu:
        this.pauseMenu.gameObject.SetActive(true);
    }

    private void HandleBoostEvent(int cooldownDuration)
    {
        StartCoroutine(DisplayCooldown(this.boostUIImage, cooldownDuration));
    }

    private void HandleShieldEvent(int cooldownDuration)
    {
        StartCoroutine(DisplayCooldown(this.shieldUIImage, cooldownDuration));
    }

    // Click Event Handlers:

    public void OnGoToMainMenuClick()
    {
        // load main menu scene:
        SceneManager.LoadSceneAsync("Menu");
    }

    public void OnResumeClick()
    {
        // unpause game:
        this.UnpauseGame();

        // hide pause menu:
        this.pauseMenu.gameObject.SetActive(false);
    }

    public void OnRestartClick()
    {
        // reload same scene:
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    // Function helpers:

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    // Coroutines:

    IEnumerator DisplayCooldown(Image img, int cooldownDuration)
    {
        float elapsedTime = 0.0f;
        while(elapsedTime < cooldownDuration)
        {
            img.fillAmount = elapsedTime / (float)cooldownDuration;
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
    }
}
