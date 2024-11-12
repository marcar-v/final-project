using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected int _enemyDamage;
    [SerializeField] protected Animator _enemyAnimator;
    [SerializeField] protected int _enemyLives;
    protected SpriteRenderer _enemySpriteRenderer;
    [SerializeField] protected float _enemySpeed;
    [SerializeField]protected Rigidbody2D _enemyRigidbody;

    [SerializeField] GameObject _enemyDeathAnim;

    bool _enemyIsDead = false;

    int _playerBulletDamage = 1;

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
            LoseLife(_playerBulletDamage);
            //LoseLifeAndHit();
            CheckLife();
        }
    }


    public virtual void LoseLife(int damage)
    {
        _enemyLives -= damage;
        _enemyAnimator.SetTrigger("HurtEnemy");
        StartCoroutine(ForceResetTrigger());

    }

    private IEnumerator ForceResetTrigger()
    {
        yield return new WaitForSeconds(_enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        _enemyAnimator.CrossFade("ThingAnim", 0.01f);
    }

    public virtual void CheckLife()
    {
        if (_enemyLives <= 0 && !_enemyIsDead)
        {
            _enemyIsDead = true;
            _enemyRigidbody.velocity = Vector2.zero;
            _enemySpriteRenderer.enabled = false;
            PlayDeadAnimation();

            Invoke("EnemyDie", 0.5f);
        }
    }

    public virtual void PlayDeadAnimation()
    {
        Instantiate(_enemyDeathAnim, transform.position, Quaternion.identity);
    }

    public virtual void EnemyDie()
    {
        Destroy(gameObject);
    }
}
