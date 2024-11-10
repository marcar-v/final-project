using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    int _bulletSpeed = 5;

    void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * _bulletSpeed;
    }

    void OnBecameInvisible()
    {
        gameObject.transform.position = new Vector3(0.03f, 0f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
    }
}
