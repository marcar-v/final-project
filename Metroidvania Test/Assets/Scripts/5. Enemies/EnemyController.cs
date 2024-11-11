using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected int _enemyDamage;
    protected Animator _enemyAnimator;
    [SerializeField] protected int _enemyLives;
    protected SpriteRenderer _enemySpriteRenderer;
    [SerializeField] protected float _enemySpeed;
    protected BoxCollider2D _enemyBoxCollider;

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
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            LoseLifeAndHit();
            CheckLife();
        }
    }

    public virtual void LoseLifeAndHit()
    {
        _enemyLives--;
    }

    public virtual void CheckLife()
    {
        if (_enemyLives == 0)
        {
            Invoke("EnemyDie", 5f);
        }
    }


    public virtual void EnemyDie()
    {
        Destroy(gameObject);
    }
}
