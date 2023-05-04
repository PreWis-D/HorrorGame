using UnityEngine;

public class EnemyFlyAnimationTransition : EnemyTransition
{
    [SerializeField] private EnemyPlayerSpottedState _state;

    private new void OnEnable()
    {
        base.OnEnable();
        _state.AnimationEnded += OnAnimationEnded;
    }

    private void OnDisable()
    {
        _state.AnimationEnded -= OnAnimationEnded;
    }

    private void OnAnimationEnded()
    {
        NeedTransit = true;
    }
}