using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    Transform _target;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        _enemyLives = 10;
    }

    public override void LoseLife(int damage)
    {
        base.LoseLife(damage);
        StartCoroutine(ForceResetTrigger());
    }

    private IEnumerator ForceResetTrigger()
    {
        yield return new WaitForSeconds(_enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        _enemyAnimator.CrossFade("IdleBoss", 0.01f);
    }
}
