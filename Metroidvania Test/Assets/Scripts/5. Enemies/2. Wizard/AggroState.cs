using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroState : MonoBehaviour
{
    StateController _stateController;

    [SerializeField] Animator anim;

    [SerializeField] float waitTimeToAttack = 1.5f;
    [SerializeField] float waitedTime = 1.5f;

    [SerializeField] BulletPool _enemyBulletPool;

    void Awake()
    {
        _stateController = GetComponent<StateController>();
    }

    private bool CanIAttack()
    {
        bool isInRange = Vector2.Distance(transform.position, _stateController.target.position) < _stateController._aggroDistance;
        bool timeToAttack = waitedTime <= 0;
        if (isInRange)
        {
            waitedTime = timeToAttack ? waitTimeToAttack : waitedTime - Time.deltaTime;
        }

        return isInRange && timeToAttack;

    }

    void Attack()
    {
        anim.Play("Wizard_Attack");
        //Invoke("Shoot", 0.75f);
    }

    public void Shoot()
    {
        _enemyBulletPool.GetComponent<BulletPool>().ShootBullet();
    }

    void Update()
    {
        if (CanIAttack())
        {
            Attack();
        }

        float _distanceToPlayer = Vector2.Distance(transform.position, _stateController.target.position);

        if (_distanceToPlayer > _stateController._aggroDistance)
        {
            _stateController.currentStates = EnemyStates.Idle;
        }
    }
}
