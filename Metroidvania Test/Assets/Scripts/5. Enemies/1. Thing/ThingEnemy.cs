using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingEnemy : EnemyController
{
    Transform _target;

    [SerializeField] Animator _animator;
    
    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        _enemyLives = 3;
        StartCoroutine(StartMovementAfterDelay(0.5f));
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }


    public override void LoseLife(int damage)
    {
        base.LoseLife(damage);
        StartCoroutine(ForceResetTrigger());
    }


    private IEnumerator ForceResetTrigger()
    {
        yield return new WaitForSeconds(_enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        _enemyAnimator.CrossFade("ThingAnim", 0.01f);
    }

}
