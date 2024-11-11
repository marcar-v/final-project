using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinCollected : MonoBehaviour
{
    GameObject _coinsText;

    private void Awake()
    {
        _coinsText = GameObject.FindGameObjectWithTag("ScoreText");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _coinsText.GetComponent<CoinManager>().TotalCoins++;
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            Destroy(gameObject, 0.5f);
        }
    }
}
