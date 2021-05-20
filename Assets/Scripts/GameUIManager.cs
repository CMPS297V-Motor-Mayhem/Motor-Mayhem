using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Ability
{
    Boost,
    Shield
}

public class GameUIManager : MonoBehaviour
{
    public Canvas gameUiMenu;
    public Canvas pauseMenu;

    [Header("UI Settings")]
    public Text scoreValueTxt;

    public Text remainingCarsValueTxt;
    public Text carsKnockedOutValueTxt;

    [Header("Abilities Settings")]
    public Image boostUIImage;

    public Image shieldUIImage;

    [Header("Game Over Menu Settings")]
    public Canvas gameOverMenu;

    public Text gameOverTitle;
    public List<Image> dividers;
    public List<Button> buttons;
    public Text scoreTxt;
    public Text bestScoreTxt;
    public Color winColor;
    public Color loseColor;

    [HideInInspector] public int score;         // needs constant feeding from elsewhere
    [HideInInspector] public int carsRemaining; // needs constant feeding from elsewhere

    // helper variables:
    private int knockedOffCars;

    private bool isBoosted;
    private bool isShielded;

    private void Start()
    {
        // initialize helper variables:
        this.knockedOffCars = 0;

        // add listeners to events:
        GameEvents.GameWinEvent.AddListener(HandleGameWinEvent);
        GameEvents.GameLoseEvent.AddListener(HandleGameLoseEvent);
        GameEvents.PauseEvent.AddListener(HandlePauseEvent);
        GameEvents.UnPauseEvent.AddListener(HandleUnPauseEvent);
        GameEvents.BoostEvent.AddListener(HandleBoostEvent);
        GameEvents.ShieldEvent.AddListener(HandleShieldEvent);
        GameEvents.KnockedOffCarEvent.AddListener(HandleKnockedOffCarEvent);
    }

    private void Update()
    {
        // update UI every frame:
        UpdateScoreUI();
        UpdateCarsRemainingUI();
    }

    // Game Events Handlers:

    private void HandleGameWinEvent()
    {
        // hide game UI menu:
        this.gameUiMenu.gameObject.SetActive(false);

        // update best score:
        BestScoreManager.SaveBestScore(score);

        // display game over menu:
        this.gameOverMenu.gameObject.SetActive(true);

        // update title:
        this.gameOverTitle.text = "You Won!";

        // update score and best score texts:
        this.scoreTxt.text = score.ToString();
        this.bestScoreTxt.text = BestScoreManager.GetBestScore().ToString();

        // color objects accordingly:
        this.ColorGameOverMenuItems(this.winColor);

        // play game win sfx
        SFXEvents.SFXGameWinEvent();
    }

    private void HandleGameLoseEvent()
    {
        // hide game UI menu:
        this.gameUiMenu.gameObject.SetActive(false);

        // update best score:
        BestScoreManager.SaveBestScore(score);

        // display game over menu:
        this.gameOverMenu.gameObject.SetActive(true);

        // update title:
        this.gameOverTitle.text = "Game Over - You Lost!";

        // update score and best score texts:
        this.scoreTxt.text = score.ToString();
        this.bestScoreTxt.text = BestScoreManager.GetBestScore().ToString();

        // color objects accordingly:
        this.ColorGameOverMenuItems(this.loseColor);

        // play game lose sfx
        SFXEvents.SFXGameLoseEvent();
    }

    private void HandlePauseEvent()
    {
        // hide game UI menu:
        this.gameUiMenu.gameObject.SetActive(false);

        // pause game:
        this.PauseGame();

        // display pause menu:
        this.pauseMenu.gameObject.SetActive(true);
    }

    private void HandleUnPauseEvent()
    {
        OnResumeClick();
    }

    private void HandleBoostEvent(float cooldownDuration)
    {
        // ensure that player isn't already boosted:
        if (!isBoosted)
            StartCoroutine(DisplayAbilityCooldown(this.boostUIImage, cooldownDuration, Ability.Boost));
    }

    private void HandleShieldEvent(float cooldownDuration)
    {
        // ensure that player isn't already shielded:
        if (!isShielded)
            StartCoroutine(DisplayAbilityCooldown(this.shieldUIImage, cooldownDuration, Ability.Shield));
    }

    private void HandleKnockedOffCarEvent()
    {
        // increment knocked off cars;
        this.knockedOffCars++;

        // update UI:
        this.carsKnockedOutValueTxt.text = this.knockedOffCars.ToString();
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

        // show game UI menu:
        this.gameUiMenu.gameObject.SetActive(true);
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

    private void UpdateScoreUI()
    {
        this.scoreValueTxt.text = this.score.ToString();
    }

    private void UpdateCarsRemainingUI()
    {
        this.remainingCarsValueTxt.text = this.carsRemaining.ToString();
    }

    // Coroutines:

    public IEnumerator DisplayAbilityCooldown(Image img, float cooldownDuration, Ability ability)
    {
        // first, make sure that the boolean value is set properly
        // to prevent "double boosting" or "double shielding"
        switch (ability)
        {
            case Ability.Boost:
                isBoosted = true;
                break;

            case Ability.Shield:
                isShielded = true;
                break;
        }

        float elapsedTime = 0.0f;
        while (elapsedTime < cooldownDuration)
        {
            img.fillAmount = elapsedTime / cooldownDuration;
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }

        // after we're done, return the boolean values to their initial value
        switch (ability)
        {
            case Ability.Boost:
                isBoosted = false;
                break;

            case Ability.Shield:
                isShielded = false;
                break;
        }
    }
}