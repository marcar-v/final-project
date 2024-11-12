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
}
