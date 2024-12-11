using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _numberOfCoinsText;
    int _totalCoinsInLevel;
    int _coinsCollected;


    public int TotalCoins
    {
        get
        {
            return _coinsCollected;
        }

        set
        {
            _coinsCollected = value;
            UpdateCoinsColleted();
        }
    }

    private void Start()
    {
        if (IsFirstLevel()) // Implementa esta lógica según tu juego
        {
            PlayerPrefs.DeleteKey("TotalCoins");
        }
        _coinsCollected = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinsColleted();
    }
    private void OnDestroy()
    {
        // Guardar las monedas al salir del nivel
        PlayerPrefs.SetInt("TotalCoins", _coinsCollected);
        PlayerPrefs.Save(); // Asegurar que se guarde
    }

    // Update is called once per frame
    void UpdateCoinsColleted()
    {
        string _coinsCollectedStr = string.Format("{0:0000}", _coinsCollected);
        _numberOfCoinsText.text = _coinsCollectedStr;
    }

    private bool IsFirstLevel()
    {
        // Verifica el índice de escena
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1;
    }
}
