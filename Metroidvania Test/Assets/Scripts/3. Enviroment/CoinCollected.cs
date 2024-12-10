using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinCollected : MonoBehaviour
{
    GameObject _coinsText;
    AudioSource _coinAudio;

    private void Awake()
    {
        _coinsText = GameObject.FindGameObjectWithTag("ScoreText");
        _coinAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _coinAudio.Play();
            _coinsText.GetComponent<CoinManager>().TotalCoins++;
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            Destroy(gameObject, 0.2f);
        }
    }
}
