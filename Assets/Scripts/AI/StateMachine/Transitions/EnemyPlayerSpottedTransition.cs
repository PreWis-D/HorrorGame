using System.Collections;
using UnityEngine;

public class EnemyPlayerSpottedTransition : EnemyTransition
{
    private Coroutine _checkEnemyInJob;

    private new void OnEnable()
    {
        base.OnEnable();

        if (Enemy == null)
            _checkEnemyInJob = StartCoroutine(CheckEnemy());
        else
            Enemy.PlayerSpotted += OnPlayerSpotted;
    }

    private void OnDisable()
    {
        if (Enemy != null)
            Enemy.PlayerSpotted -= OnPlayerSpotted;

        if (_checkEnemyInJob != null)
            StopCoroutine(_checkEnemyInJob);
    }

    private void OnPlayerSpotted()
    {
        Enemy.StopMove();
        NeedTransit = true;
    }

    private IEnumerator CheckEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (Enemy == null)
        {
            yield return wait;
        }

        Enemy.PlayerSpotted += OnPlayerSpotted;
    }
}