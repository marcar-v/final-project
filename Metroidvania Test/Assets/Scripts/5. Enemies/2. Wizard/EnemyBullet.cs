using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    int _bulletSpeed = 5;
    Animator _bulletAnimator;
    Rigidbody2D _rb;

    private void Awake()
    {
        _bulletAnimator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _rb.velocity = Vector2.left * _bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _bulletAnimator.SetTrigger("BulletHit");

            StartCoroutine(DeactivateBullet());
        }
    }
    private void OnBecameInvisible()
    {
        gameObject.transform.position = new Vector3(0.3f, 0f);
        gameObject.SetActive(false);
    }
    IEnumerator DeactivateBullet()
    {
        yield return new WaitForSeconds(_bulletAnimator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
