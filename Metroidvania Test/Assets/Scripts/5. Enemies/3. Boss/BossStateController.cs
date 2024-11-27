using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BossStateController;

public class BossStateController : MonoBehaviour
{
    public enum EnemyState { Move, Attack }
    [SerializeField] private EnemyState currentState = EnemyState.Move;

    [Header("Movement Settings")]
    private EnemyController movementManager;

    [Header("Attack")]
    private BossShooting _bossShooting;

    private void Start()
    {
        // Asegúrate de tener la referencia al MovementManager
        if (movementManager == null)
        {
            movementManager = GetComponent<EnemyController>();
        }
        if (_bossShooting == null)
        {
            _bossShooting = GetComponent<BossShooting>();
        }

        movementManager.StartCoroutine(movementManager.StartMovementAfterDelay(3f));

        // Comienza el estado Move al iniciar
        ChangeState(EnemyState.Move);
    }
    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    private void Update()
    {
        // Gestionamos el estado en cada frame
        switch (currentState)
        {
            case EnemyState.Move:
                HandleMoveState();
                break;
            case EnemyState.Attack:
                HandleAttackState();
                break;
        }
    }

    private void HandleMoveState()
    {
        movementManager.EnemyMovement();  // El enemigo se mueve

        //Condición para pasar al estado de ataque(ejemplo: si está cerca del jugador)
        if (movementManager.IsAtWaypoint(2) && _bossShooting.CanIAttack(true))
        {
            ChangeState(EnemyState.Attack);
        }
    }

    private void HandleAttackState()
    {
        // Iniciar la corutina de ataque
        StartCoroutine(PerformAttackAndReturnToMove(3f));  // 3 segundos de ataque, ajusta el tiempo como necesites
    }

    private IEnumerator PerformAttackAndReturnToMove(float attackDuration)
    {
        _bossShooting.Attack(); // Ejecutar la lógica de ataque

        // Espera durante el tiempo del ataque
        yield return new WaitForSeconds(attackDuration);

        // Después de que haya pasado el tiempo de ataque, vuelve al estado Move
        ChangeState(EnemyState.Move);
    }
}