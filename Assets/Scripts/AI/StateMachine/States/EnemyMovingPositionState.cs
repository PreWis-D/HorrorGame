using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingPositionState : EnemyState
{
    private void OnEnable()
    {
        Enemy.TryMoveTargetPosition();
    }
}
