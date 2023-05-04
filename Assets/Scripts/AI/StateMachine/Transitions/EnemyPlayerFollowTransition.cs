using UnityEngine;

public class EnemyPlayerFollowTransition : EnemyTransition
{
    [SerializeField] private EnemyPlayerFollowState _followState;

    private new void OnEnable()
    {
        base.OnEnable();
        _followState.PlayerReached += OnPlayerReached;
        _followState.PlayerMissed += OnPlayerMissed;
    }

    private void OnDisable()
    {
        _followState.PlayerReached -= OnPlayerReached;
        _followState.PlayerMissed -= OnPlayerMissed;
    }

    private void OnPlayerReached()
    {
        NeedTransit = true;
    }

    private void OnPlayerMissed()
    {
        NeedAlternativeTransit = true;
    }
}