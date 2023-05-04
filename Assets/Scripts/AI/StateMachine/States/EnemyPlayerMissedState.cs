using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPlayerMissedState : EnemyState
{
    private Coroutine _endAnimationInJob;

    public event UnityAction AnimationEnded;

    private void OnEnable()
    {
        Enemy.ChangePlayerSpottedAnimation(false);
        _endAnimationInJob = StartCoroutine(EndAnimation());
    }

    private void OnDisable()
    {
        if (_endAnimationInJob != null)
            StopCoroutine(_endAnimationInJob);
    }

    private IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(Enemy.Delay);

        Enemy.StopMove();
        AnimationEnded?.Invoke();
    }
}
