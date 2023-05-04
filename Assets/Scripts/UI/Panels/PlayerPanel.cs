using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : Panel
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        Hide();
    }
}
