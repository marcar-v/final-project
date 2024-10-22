using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManagerInstance { get; private set; }

    [SerializeField] AudioSource _gameplayMusic;
    [SerializeField] AudioSource _mainMenuMusic;

    private void Awake()
    {
        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void GameplayMusic()
    {
        _gameplayMusic.Play();
        _mainMenuMusic.Stop();
    }

    public void MainMenuMusic()
    {
        _mainMenuMusic.Play();
        _gameplayMusic.Stop();
    }
}
