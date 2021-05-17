using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Canvas pauseMenu;

    [Header("Game Over Menu Settings")]
    public Canvas gameOverMenu;
    public Text gameOverTitle;
    public List<Image> dividers;
    public List<Button> buttons;
    public Text scoreTxt;
    public Text bestScoreTxt;
    public Color winColor;
    public Color loseColor;

    [Header("Abilities Settings")]
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

    private void Update()
    {
        // debugging:
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameEvents.PauseEvent.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GameEvents.GameWinEvent.Invoke(20);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            GameEvents.GameLoseEvent.Invoke(10);
        }
    }

    // Game Events Handlers:

    private void HandleGameWinEvent(int score)
    {
        // update best score:
        BestScoreManager.SaveBestScore(score);

        // update score:
        this.score = score;

        // display game over menu:
        this.gameOverMenu.gameObject.SetActive(true);

        // update title:
        this.gameOverTitle.text = "You Won!";

        // update score and best score texts:
        this.scoreTxt.text = score.ToString();
        this.bestScoreTxt.text = BestScoreManager.GetBestScore().ToString();

        // color objects accordingly:
        this.ColorGameOverMenuItems(this.winColor);
    }

    private void HandleGameLoseEvent(int score)
    {
        // update best score:
        BestScoreManager.SaveBestScore(score);

        // update score:
        this.score = score;

        // display game over menu:
        this.gameOverMenu.gameObject.SetActive(true);

        // update title:
        this.gameOverTitle.text = "Game Over - You Lost!";

        // update score and best score texts:
        this.scoreTxt.text = score.ToString();
        this.bestScoreTxt.text = BestScoreManager.GetBestScore().ToString();

        // color objects accordingly:
        this.ColorGameOverMenuItems(this.loseColor);
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
        // unpause game:
        this.UnpauseGame();

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

    private void ColorGameOverMenuItems(Color color)
    {
        // 1. color title:
        this.gameOverTitle.color = color;

        // 2. color dividers:
        foreach (Image divider in this.dividers)
        {
            divider.color = color;
        }

        // 3. color scores:
        this.scoreTxt.color = color;
        this.bestScoreTxt.color = color;

        // 4. color buttons:
        foreach (Button btn in this.buttons)
        {
            btn.GetComponent<Image>().color = color;
        }
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
