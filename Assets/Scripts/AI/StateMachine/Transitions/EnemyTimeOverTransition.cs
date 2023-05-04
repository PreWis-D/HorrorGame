using System.Collections;
using UnityEngine;

public class EnemyTimeOverTransition : EnemyTransition
{
    [SerializeField] private EnemyIdleState _enemyIdleState;

    private new void OnEnable()
    {
        base.OnEnable();
        _enemyIdleState.TimeOver += OnTimeOver;
    }

    private void OnDisable()
    {
        _enemyIdleState.TimeOver -= OnTimeOver;
    }

    private void OnTimeOver()
    {
        NeedTransit = true;
    }
}