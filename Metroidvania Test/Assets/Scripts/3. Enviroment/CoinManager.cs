using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager coinManagerInstance;
    public TextMeshProUGUI _numberOfCoinsText;
    int _coinsCollected;
    public int coinsCollected { get; private set; }

    private void Awake()
    {
        if (coinManagerInstance == null)
        {
            coinManagerInstance = this;  // Asigna la instancia de CoinManager
        }
        else
        {
            Destroy(gameObject);  // Si ya existe, destruye el objeto duplicado
        }
    }

    public int TotalCoins
    {
        get { return _coinsCollected; }
        set
        {
            _coinsCollected = value;
            UpdateCoinsColleted();
            PlayerPrefs.SetInt("TotalCoins", _coinsCollected); // Actualiza PlayerPrefs
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        _coinsCollected = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinsColleted();
    }

    public void CollectCoin()
    {
        coinsCollected++;
        PlayerPrefs.SetInt("TotalCoins", coinsCollected);
        PlayerPrefs.Save();
        Debug.Log("Monedas guardadas en PlayerPrefs: " + coinsCollected);
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
}
