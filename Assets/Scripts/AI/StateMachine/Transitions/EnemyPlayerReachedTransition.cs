using System.Collections;
using UnityEngine;

public class EnemyPlayerReachedTransition : EnemyTransition
{
    private float _cooldawn = 1f;

    private new void OnEnable()
    {
        base.OnEnable();
        if (Enemy == null)
            StartCoroutine(CheckEnemy());
        else
            StartCoroutine(CheckPlayer());
    }

    private void OnPlayerReached()
    {
        NeedTransit = true;
    }

    private IEnumerator CheckEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (Enemy == null)
        {
            yield return wait;
        }

        StartCoroutine(CheckPlayer());
    }

    private IEnumerator CheckPlayer()
    {
        WaitForSeconds wait = new WaitForSeconds(_cooldawn);

        while (Enemy.IsPlayerReached == false)
        {
            yield return wait;
        }

        OnPlayerReached();
    }
}