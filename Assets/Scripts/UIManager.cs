using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject howToPlayScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject creditsScreen;

    [SerializeField] private GameObject MainMenuContinueButton;
    public static UIManager Instance { get; private set; }

    private bool isFromPauseMenu;

    private void Awake()
    {
       // print("uimanager");
        if (Instance == null)
            Instance = this;
        Time.timeScale = 1;
        startMenu.SetActive(true);

        if (PlayerPrefs.GetInt("HasSaveState", 0) != 0)
        {
            MainMenuContinueButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
    }

    private void Update()
    {
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }

    public void StartButtonClicked()
    {
        GameHandler.Instance.playerCrossHair.SetActive(true);

        Time.timeScale = 1;
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        GameHandler.Instance.LoadPlayerStartingPosition();
        GameHandler.Instance.HideMouseCursor();
       // print("scene change");
    }

    public void ContinueButtonClicked()
    {
        if (PlayerPrefs.GetInt("HasSaveState", 0) != 0) 
        {
            GameHandler.Instance.playerCrossHair.SetActive(true);

            Time.timeScale = 1;
            GameHandler.Instance.LoadPlayerPosition();
            startMenu.SetActive(false);
            GameHandler.Instance.HideMouseCursor();
        }
    }

    public void ShowPauseMenu()
    {
        isFromPauseMenu = true;

        GameHandler.Instance.playerCrossHair.SetActive(false);

        GameHandler.Instance.ShowMouseCursor();
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        isFromPauseMenu = false;

        GameHandler.Instance.playerCrossHair.SetActive(true);

        Time.timeScale = 1;
        GameHandler.Instance.HideMouseCursor();
        pauseMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        GameHandler.Instance.ShowMouseCursor();
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }

    public void ShowGameOverMenu()
    {
        GameHandler.Instance.playerCrossHair.SetActive(false);

        GameHandler.Instance.ShowMouseCursor();
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void ShowHowToPlayScreen()
    {
        startMenu.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void HideHowToPlayScreen()
    {
        howToPlayScreen.SetActive(false);
        startMenu.SetActive(true);
    }

    public void ShowSettingsScreen()
    {
        startMenu.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void HideSettingsScreen()
    {
        settingsScreen.SetActive(false);
        if(!isFromPauseMenu)
            startMenu.SetActive(true);
    }

    public void HideSettingsScreenFromPauseMenu()
    {
        settingsScreen.SetActive(false);
    }

    public void ShowCreditsScreen()
    {
        creditsScreen.SetActive(true);
        gameOverMenu.SetActive(false);
    }
    public void HideCreditsScreen()
    {
        creditsScreen.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}
