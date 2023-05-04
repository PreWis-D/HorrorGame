using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPlayerSpottedState : EnemyState
{
    private Coroutine _endAnimationInJob;

    public event UnityAction AnimationEnded;

    private void OnEnable()
    {
        Enemy.ChangePlayerSpottedAnimation(true);
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

        AnimationEnded?.Invoke();
    }
}