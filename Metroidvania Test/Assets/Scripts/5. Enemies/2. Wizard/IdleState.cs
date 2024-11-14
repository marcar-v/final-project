using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour
{
    StateController _stateController;

    void Awake()
    {
        _stateController = GetComponent<StateController>();
    }

    void Update()
    {
        float _distanceToPlayer = Vector2.Distance(transform.position, _stateController.target.position);

        if (_distanceToPlayer <= _stateController._aggroDistance)
        {
            _stateController.currentStates = EnemyStates.Aggro;
        }
    }
}
