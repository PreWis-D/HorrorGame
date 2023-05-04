using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyIdleState : EnemyState
{
    private Coroutine _waitInJob;

    public event UnityAction TimeOver;

    private void OnEnable()
    {
        TryStopCoroutine();
        _waitInJob = StartCoroutine(CheckEnemy());
    }

    private void OnDisable()
    {
        TryStopCoroutine();
    }

    private void TryStopCoroutine()
    {
        if (_waitInJob != null)
            StopCoroutine(_waitInJob);
    }

    private IEnumerator CheckEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (Enemy == null)
            yield return wait;

        Enemy.StopMove();
        _waitInJob = StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(Enemy.WaitingTime);

        TimeOver?.Invoke();
    }
}