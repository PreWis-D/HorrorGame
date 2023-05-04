using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingPositionDistanceTransition : EnemyTransition
{
    [SerializeField] private float _distance = 0.5f;

    private void Update()
    {
        if (Enemy.EnemyMover.NavMeshAgent.enabled && Enemy.EnemyMover.NavMeshAgent.remainingDistance <= _distance)
        {
            Enemy.DropTargetPosition();
            NeedTransit = true;
        }
    }
}
