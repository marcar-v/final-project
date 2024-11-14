using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRecovered : MonoBehaviour
{
    private int lives = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(LifeAdded())
            {
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
