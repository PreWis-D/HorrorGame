using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerMissedTransition : EnemyTransition
{
    [SerializeField] private EnemyPlayerMissedState _missedState;

    private new void OnEnable()
    {
        base.OnEnable();
        _missedState.AnimationEnded += OnAnimationEnded;
    }

    private void OnDisable()
    {
        _missedState.AnimationEnded -= OnAnimationEnded;
    }

    private void OnAnimationEnded()
    {
        NeedTransit = true;
    }
}
