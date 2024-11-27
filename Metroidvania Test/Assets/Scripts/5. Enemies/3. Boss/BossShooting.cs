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
    [SerializeField] Transform attackWaypoint;
    private bool isAtCorrectWaypoint = false;

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

    public void HandleAttackLogic(Vector2 enemyPosition)
    {
        // Verificamos si el enemigo está en el waypoint correcto
        if (Vector2.Distance(enemyPosition, attackWaypoint.position) < 0.5f)
        {
            if (!isAtCorrectWaypoint)  // Solo iniciar el contador si no ha comenzado
            {
                isAtCorrectWaypoint = true; // El enemigo llegó al waypoint de ataque
                waitedTime = waitTimeToAttack;  // Iniciar el contador de espera
            }
        }
        else
        {
            isAtCorrectWaypoint = false;  // El enemigo no está en el waypoint
        }

        // Si ha llegado al waypoint, comenzamos a descontar el tiempo para el siguiente ataque
        if (isAtCorrectWaypoint)
        {
            waitedTime -= Time.deltaTime; // Comienza a contar el tiempo para el siguiente ataque
        }
    }

    public bool IsAtCorrectWaypoint(Vector2 enemyPosition)
    {
        // Este método verifica si el enemigo ha llegado al waypoint de ataque
        return Vector2.Distance(enemyPosition, attackWaypoint.position) < 0.01f;  // Ajusta el umbral de distancia
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
}
