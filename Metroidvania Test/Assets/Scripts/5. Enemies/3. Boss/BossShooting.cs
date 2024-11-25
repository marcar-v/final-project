using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public static BossShooting _bossShootingInstance;

    Animator _animator;

    [SerializeField] float waitTimeToAttack = 5f;
    [SerializeField] float waitedTime = 5f;

    [SerializeField] BulletPool _enemyBulletPool;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public bool CanIAttack(bool isAtCorrectWaypoint)
    {
        if (!isAtCorrectWaypoint) return false; // Solo atacar si está en el waypoint correcto

        waitedTime -= Time.deltaTime;
        if (waitedTime <= 0)
        {
            waitedTime = waitTimeToAttack;
            return true;
        }

        return false;

    }

    public void Attack()
    {
        _animator.Play("AttackBoss");
        Shoot();
    }

    public void Shoot()
    {
        _enemyBulletPool.GetComponent<BulletPool>().ShootBullet();
    }

    //void Update()
    //{
    //    if (CanIAttack())
    //    {
    //        Attack();
    //    }
    //}
}
