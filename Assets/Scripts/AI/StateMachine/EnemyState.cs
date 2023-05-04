using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private List<EnemyTransition> _transitions;

    protected Enemy Enemy { get; set; }

    public void Enter(Enemy enemy)
    {
        if (enabled == false)
        {
            Enemy = enemy;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Enemy);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
            if (transition.NeedAlternativeTransit)
                return transition.TargetAlternativeState;
        }

        return null;
    }
}
