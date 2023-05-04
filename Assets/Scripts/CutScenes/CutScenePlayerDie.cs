using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CutScenePlayerDie : MonoBehaviour
{
    [SerializeField] private Player _player;

    public event UnityAction CutSceneEnded;

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
        _player.GetComponent<Animator>().SetTrigger("Die");
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);

        CutSceneEnded?.Invoke();
    }
}
