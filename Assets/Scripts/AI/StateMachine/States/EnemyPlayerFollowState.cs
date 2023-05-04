using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPlayerFollowState : EnemyState
{
    private float _counter = 0;
    private bool _isPlayerMissed;

    public event UnityAction PlayerReached;
    public event UnityAction PlayerMissed;

    private void OnEnable()
    {
        _counter = 0;
        Enemy.PlayerSpotted += OnPlayerSpotted;
        Enemy.PlayerMissed += OnPlayerMissed;
    }

    private void OnDisable()
    {
        _counter = 0;
        Enemy.PlayerSpotted -= OnPlayerSpotted;
        Enemy.PlayerMissed -= OnPlayerMissed;
    }

    private void Update()
    {
        if (_isPlayerMissed == false)
        {
            Enemy.TryFollowPlayer();

            if (Enemy.IsPlayerReached)
            {
                PlayerReached?.Invoke();
                OnDisable();
            }
            _counter = 0;
        }
        else
        {
            _counter += Time.deltaTime;

            if (_counter > Enemy.FollowTime)
                PlayerMissed?.Invoke();
        }
    }

    private void OnPlayerSpotted()
    {
        _isPlayerMissed = false;
    }

    private void OnPlayerMissed()
    {
        _isPlayerMissed = true;
    }
}