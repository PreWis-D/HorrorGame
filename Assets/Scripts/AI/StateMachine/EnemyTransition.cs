using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;
    [SerializeField] private EnemyState _targetAlternativeState;

    protected Enemy Enemy { get; private set; }
    public EnemyState TargetState => _targetState;
    public EnemyState TargetAlternativeState => _targetAlternativeState;
    public bool NeedTransit { get; protected set; }
    public bool NeedAlternativeTransit { get; protected set; }

    public void Init(Enemy enemy)
    {
        Enemy = enemy;
    }

    protected void OnEnable()
    {
        NeedTransit = false;
        NeedAlternativeTransit = false;
    }

    public void DisableTransit()
    {
        NeedTransit = false;
    }
}
