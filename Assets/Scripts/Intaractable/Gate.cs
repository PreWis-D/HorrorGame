using UnityEngine;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    [SerializeField] private KeyHole[] _keyHoles;
    [SerializeField] private int _targetCount;
    [SerializeField] private Transform _door;
    [SerializeField] private Vector3 _targetRotation;
    [SerializeField] private float _duration;
    [SerializeField] private IntaractableSpawner _spawner;

    private int _currentCount = 0;
    private int _index = 0;

    private void OnEnable()
    {
        for (int i = 0; i < _keyHoles.Length; i++)
            _keyHoles[i].AnimationEnded += OnExecuted;

        _spawner.Spawned += OnSpawned;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _keyHoles.Length; i++)
            _keyHoles[i].AnimationEnded -= OnExecuted;

        _spawner.Spawned -= OnSpawned;
    }

    private void OnExecuted()
    {
        _currentCount++;

        if (_currentCount >= _targetCount)
            Open();
    }

    private void Open()
    {
        _door.transform.DOLocalRotate(_targetRotation, _duration).SetEase(Ease.Linear);
    }

    private void OnSpawned(Intaractable intaractable)
    {
        if (intaractable.TryGetComponent(out Key key) && _index < _keyHoles.Length)
        {
            _keyHoles[_index].SetKey(key);
            _index++;
        }
    }
}
