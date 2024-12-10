using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRecovered : MonoBehaviour
{
    private int lives = 3;
    AudioSource _liveRecoveredSound;

    private void Awake()
    {
        _liveRecoveredSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(LifeAdded())
            {
                _liveRecoveredSound.Play();
                Destroy(gameObject);
            }
        }
    }


    bool LifeAdded()
    {
        if(lives >= 5)
        {
            return false;
        }

        Lives._livesInstance.ActivateLife(lives);
        lives += 1;

        return true;
    }
}
