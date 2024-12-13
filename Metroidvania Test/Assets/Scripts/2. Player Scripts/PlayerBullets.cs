using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    int _bulletSpeed = 5;
    Animator _bulletAnimator;

    private void Awake()
    {
        _bulletAnimator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * _bulletSpeed;
    }

    void OnBecameInvisible()
    {
        gameObject.transform.position = new Vector3(0.03f, 0f);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _bulletAnimator.SetTrigger("BulletHit");

            StartCoroutine(DeactivateBullet());
        }
    }

    IEnumerator DeactivateBullet()
    {
        yield return new WaitForSeconds(_bulletAnimator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
