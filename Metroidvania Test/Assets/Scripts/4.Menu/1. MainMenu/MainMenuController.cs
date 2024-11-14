using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject _optionsCanvas;
    [SerializeField] GameObject _mainMenuCanvas;
    public void PlayGame()
    {
        AudioManager.audioManagerInstance.ClickSound();
        AudioManager.audioManagerInstance.GameplayMusic();
        TransitionController._transitionInstance.LoadNextScene();
    }

    public void OptionsMenu()
    {
        AudioManager.audioManagerInstance.ClickSound();
        _optionsCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.audioManagerInstance.ClickSound();
        Application.Quit();
    }
}
