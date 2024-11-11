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

    public void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        _waitTime = startWaitTime;
    }

    public void FixedUpdate()
    {
        EnemyMovement();
    }
    public void EnemyMovement()
    {
        StartCoroutine(CheckEnemyMovement());

        _enemySpeed = 0.5f;

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[_i].transform.position, _enemySpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoints[_i].transform.position) < 0.5f)
        {
            if (_waitTime <= 0)
            {
                Debug.Log("Current Wait Time: " + _waitTime);
                if (wayPoints[_i] != wayPoints[wayPoints.Length - 1])
                {
                    Debug.Log("Current Waypoint Index:" + _i);
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

    public virtual IEnumerator CheckEnemyMovement()
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
        //else if (transform.position.x == _actualPosition.x)
        //{
        //    _enemyAnimator.Play("ThingAnim");
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * jumpForce);
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
            //destroyParticle.SetActive(true);
            _enemySpriteRenderer.enabled = false;
            Invoke("EnemyDie", 0.2f);
        }
    }

    public virtual void EnemyDie()
    {
        Destroy(gameObject);
    }
}
