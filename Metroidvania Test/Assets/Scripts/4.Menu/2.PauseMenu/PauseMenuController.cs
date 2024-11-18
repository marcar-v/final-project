using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _optionsPanel;
    [SerializeField] GameObject _player;

    public void PausePanel()
    {
        //_player.GetComponentInChildren<PlayerShootController>().enabled = false;
        //_player.GetComponent<PlayerController>().enabled = false;

        Time.timeScale = 0f;
        AudioManager.audioManagerInstance.ClickSound();
        _pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        _player.GetComponentInChildren<PlayerShootController>().enabled = true;
        _player.GetComponent<PlayerController>().enabled = true;


        Time.timeScale = 1f;
        AudioManager.audioManagerInstance.ClickSound();
        _pausePanel.SetActive(false);
    }

    public void OptionsMenu()
    {
        //_player.GetComponentInChildren<PlayerShootController>().enabled = false;
        //_player.GetComponent<PlayerMove>().enabled = false;

        Time.timeScale = 0f;
        AudioManager.audioManagerInstance.ClickSound();
        _pausePanel.SetActive(false);
        _optionsPanel.SetActive(true);
    }

    public void ReturnMainMenu()
    {
        //_player.GetComponentInChildren<PlayerShootController>().enabled = false;
        //_player.GetComponent<PlayerMove>().enabled = false;

        AudioManager.audioManagerInstance.ClickSound();
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
        AudioManager.audioManagerInstance.MainMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }
}
