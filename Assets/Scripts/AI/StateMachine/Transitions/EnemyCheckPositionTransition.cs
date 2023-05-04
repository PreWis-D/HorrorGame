using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckPositionTransition : EnemyTransition
{
    [SerializeField] private EnemySearchPositionState _enemySearchPositionState;

    private new void OnEnable()
    {
        base.OnEnable();
        _enemySearchPositionState.PositionFound += OnPositionChoised;
    }

    private void OnDisable()
    {
        _enemySearchPositionState.PositionFound -= OnPositionChoised;
    }

    private void OnPositionChoised()
    {
        NeedTransit = true;  
    }
}
