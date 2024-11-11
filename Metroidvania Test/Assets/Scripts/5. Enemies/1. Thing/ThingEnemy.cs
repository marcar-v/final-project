using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingEnemy : EnemyController
{
    Transform _target;

    [Header("Movement")]
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float startWaitTime = 1f;
    private float _waitTime;
    private int _i = 0;
    private Vector2 _actualPosition;

    bool _enemyIsDead = false;

    [SerializeField] Animator _animator;
    
    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        _waitTime = startWaitTime;
        _enemyLives = 2;
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }
    private void EnemyMovement()
    {
        StartCoroutine(CheckEnemyMovement());

        _enemySpeed = 0.5f;

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[_i].transform.position, _enemySpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoints[_i].transform.position) < 0.5f)
        {
            if (_waitTime <= 0)
            {
                if (wayPoints[_i] != wayPoints[wayPoints.Length - 1])
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

    public override void LoseLifeAndHit()
    {
        base.LoseLifeAndHit();
        _animator.SetTrigger("HurtEnemy");
        StartCoroutine(ForceResetTrigger());
    }

    public override void CheckLife()
    {
        base.CheckLife();
        if (_enemyLives == 0 && !_enemyIsDead)
        {
            _enemyIsDead = true;
            _animator.SetTrigger("EnemyDeath");
            StartCoroutine(DeactiveAfterAnimation());
        }
    }
    private IEnumerator ForceResetTrigger()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        _animator.CrossFade("ThingAnim", 0.01f); // Reemplaza "Idle" con el nombre exacto del estado Idle en tu Animator
    }

    private IEnumerator DeactiveAfterAnimation()
    {
        float _animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log("Duración de la animación de muerte: " + _animationLength);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Animación de muerte terminada");
        gameObject.SetActive(false);
    }

    public override void EnemyDie()
    {
        base.EnemyDie();
    }
}
