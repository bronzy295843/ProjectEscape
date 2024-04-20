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

    [SerializeField] private GameObject MainMenuContinueButton;
    public static UIManager Instance { get; private set; }

    private bool startButtonClicked;
    private bool continueButtonClicked;

    private void Awake()
    {
        print("uimanager");
        if (Instance == null)
            Instance = this;
        Time.timeScale = 1;
        startMenu.SetActive(true);

        if (PlayerPrefs.GetInt("HasSaveState", 0) != 0)
        {
            MainMenuContinueButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
        //SceneManager.LoadScene(1);

        //DontDestroyOnLoad(this.gameObject);

    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if(SceneManager.GetActiveScene().buildIndex == 1)
    //    {
    //        if(startButtonClicked)
    //        {
    //            GameHandler.Instance.LoadPlayerStartingPosition();
    //        }
    //        else if(continueButtonClicked)
    //        {
    //            GameHandler.Instance.LoadPlayerPosition();
    //        }
    //        GameHandler.Instance.HideMouseCursor();
    //    }
    //    print("scene changed");
    //}

    private void Update()
    {
        //print("playing");
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
        print("scene change");
        //SceneManager.LoadScene(1);
        //startButtonClicked = true;
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
        //pauseMenu.SetActive(false);
        //startMenu.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void ShowGameOverMenu()
    {
        GameHandler.Instance.ShowMouseCursor();
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}
