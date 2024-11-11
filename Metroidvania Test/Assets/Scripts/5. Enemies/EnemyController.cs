using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected int _enemyDamage;
    protected Animator _enemyAnimator;
    protected int _enemyLives;
    protected SpriteRenderer _enemySpriteRenderer;
    protected float _enemySpeed;

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerDamaged>().PlayerIsDamaged(_enemyDamage);
        }
    }
}
