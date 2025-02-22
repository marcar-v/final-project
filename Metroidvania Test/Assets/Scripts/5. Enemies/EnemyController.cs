using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    protected int _enemyDamage = 1;
    [SerializeField] protected Animator _enemyAnimator;
    [SerializeField] protected int _enemyLives;
    protected SpriteRenderer _enemySpriteRenderer;
    [SerializeField] protected float _enemySpeed;
    [SerializeField]protected Rigidbody2D _enemyRigidbody;

    [SerializeField] GameObject _enemyDeathAnim;

    bool _enemyIsDead = false;
    [SerializeField] AudioSource _enemyDeathSound;
    int _playerBulletDamage = 1;

    [Header("Movement")]
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float startWaitTime = 1f;
    private float _waitTime;
    private int _i = 0;
    private Vector2 _actualPosition;
    protected float _initialWaitTime;
    protected bool _canMove = false;

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        _waitTime = startWaitTime;
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
            CheckLife();
        }
    }

    #region EnemyMovement

    public virtual IEnumerator StartMovementAfterDelay(float initialWaitTime)
    {
        _initialWaitTime = initialWaitTime;
        yield return new WaitForSeconds(_initialWaitTime);
        _canMove = true;
    }

    public virtual void EnemyMovement()
    {

        if(!_canMove)
        {
            return;
        }

        StartCoroutine(CheckEnemyMovement());

        _enemySpeed = 0.5f;

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[_i].transform.position, _enemySpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoints[_i].transform.position) < 0.5f)
        {
            if (_waitTime <= 0)
            {
                if (_i < wayPoints.Length - 1/*wayPoints[_i] != wayPoints[wayPoints.Length - 1]*/)
                {
                    _i++;
                }
                else
                {
                    _i = 0;
                }
                _waitTime = startWaitTime;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }
    }

    private IEnumerator CheckEnemyMovement()
    {
        _actualPosition = transform.position;
        yield return new WaitForSeconds(0.5f);

        _enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (transform.position.x > _actualPosition.x)
        {
            _enemySpriteRenderer.flipX = true;
        }
        else if (transform.position.x < _actualPosition.x)
        {
            _enemySpriteRenderer.flipX = false;
        }
    }
    public bool IsAtWaypoint(int waypointIndex)
    {
        if (waypointIndex < 0 || waypointIndex >= wayPoints.Length)
        {
            Debug.LogWarning("El �ndice del waypoint est� fuera de rango.");
            return false;
        }

        return Vector2.Distance(transform.position, wayPoints[waypointIndex].position) < 0.5f;
    }

    public void StopMovement()
    {
        _canMove = false;
    }
    #endregion
    public virtual void LoseLife(int damage)
    {
        _enemyLives -= damage;
        _enemyAnimator.SetTrigger("HurtEnemy");
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
        _enemyDeathSound.Play();
        Instantiate(_enemyDeathAnim, transform.position, Quaternion.identity);
    }

    public virtual void EnemyDie()
    {
        Destroy(gameObject);
    }
}
