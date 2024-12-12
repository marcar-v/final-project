using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentCoinsText;
    [SerializeField] PlayerDamaged _playerDamaged;


    public void Start()
    {
        // Actualiza las monedas cuando el panel se muestra
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        Debug.Log("MonedasStart: " + CoinManager.coinManagerInstance.coinsCollected);
        _currentCoinsText.text = totalCoins.ToString("D2");
    }

    public void RestartGame()
    {
        _playerDamaged.ResetCollisionAfterDeath();
        TransitionController._transitionInstance.ReloadCurrentScene();
        AudioManager.audioManagerInstance.ClickSound();
    }

    public void QuitButton()
    {
        AudioManager.audioManagerInstance.ClickSound();
        Application.Quit();
    }
}
