using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _rb.velocity = new Vector2(5f, 0f);
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
