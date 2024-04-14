using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    [SerializeField] private GameObject MainMenuContinueButton;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Time.timeScale = 0;
        startMenu.SetActive(true);

        if (PlayerPrefs.GetInt("HasSaveState", 0) != 0)
        {
            MainMenuContinueButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }

    public void StartButtonClicked()
    {
        Time.timeScale = 1;
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        GameHandler.Instance.LoadPlayerStartingPosition();
        GameHandler.Instance.HideMouseCursor();
    }

    public void ContinueButtonClicked()
    {
        if (PlayerPrefs.GetInt("HasSaveState", 0) != 0) 
        {
            Time.timeScale = 1;
            GameHandler.Instance.LoadPlayerPosition();
            startMenu.SetActive(false);
            GameHandler.Instance.HideMouseCursor();
        }
    }

    public void ShowPauseMenu()
    {
        GameHandler.Instance.ShowMouseCursor();
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        GameHandler.Instance.HideMouseCursor();
        pauseMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        GameHandler.Instance.ShowMouseCursor();
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void ShowGameOverMenu()
    {
        GameHandler.Instance.ShowMouseCursor();
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}
