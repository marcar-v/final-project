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

    // Update is called once per frame
    void UpdateCoinsColleted()
    {
        string _coinsCollectedStr = string.Format("{0:0000}", _coinsCollected);
        _numberOfCoinsText.text = _coinsCollectedStr;
    }
}
