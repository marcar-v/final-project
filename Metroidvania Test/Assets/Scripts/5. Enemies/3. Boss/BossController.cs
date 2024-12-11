using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    Transform _target;

    public delegate void BossDeathEvent();
    public static event BossDeathEvent OnBossDeath;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        _enemyLives = 20;
    }

    public override void LoseLife(int damage)
    {
        base.LoseLife(damage);
        StartCoroutine(ForceResetTrigger());
    }

    public override void EnemyDie()
    {
        OnBossDeath?.Invoke();
        base.EnemyDie();
    }

    private IEnumerator ForceResetTrigger()
    {
        yield return new WaitForSeconds(_enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        _enemyAnimator.CrossFade("IdleBoss", 0.01f);
    }
}
