using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;

    public void PausePanel()
    {
        Time.timeScale = 0f;
        _pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioManager.audioManagerInstance.ClickSound();
        _pausePanel.SetActive(false);
    }
    public void ReturnMainMenu()
    {
        AudioManager.audioManagerInstance.ClickSound();
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
        AudioManager.audioManagerInstance.MainMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        AudioManager.audioManagerInstance.ClickSound();
        Application.Quit();
    }
}
