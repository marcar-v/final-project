using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRecovered : MonoBehaviour
{
    int _maxLives = 5;
    int _currentLives;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_currentLives < _maxLives)
            {
                Lives._livesInstance.LivesAdd(1);
                Destroy(gameObject);
            }
        }
    }
}
