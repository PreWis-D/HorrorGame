using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class KeyHole : Intaractable
{
    [SerializeField] private Key _key;

    [Header("AnimationSetting")]
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _targetRotation;
    [SerializeField] private float _durationMove;
    [SerializeField] private float _durationRotate;

    private AudioSource _audioSource;

    public Key Key => _key;

    public event UnityAction AnimationEnded;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Execute()
    {
        base.Execute();
        ChangeDescription("");
        StartAnimation();
    }

    public void SetKey(Key key)
    {
        _key = key;
    }

    private void StartAnimation()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        _key.ChangeTakedState(false);
        ChangeAppliedState(true);
        _key.transform.parent = transform;
        Sequence sequence = DOTween.Sequence();
        _key.transform.SetLocalPositionAndRotation(_startPosition, Quaternion.Euler(_startRotation));
        sequence.Append(_key.transform.DOLocalMove(_targetPosition, _durationMove).SetEase(Ease.Linear));
        sequence.Append(_key.transform.DOLocalRotate(_targetRotation, _durationRotate).SetEase(Ease.Linear));
        sequence.OnComplete(OnDone);
    }

    private void OnDone()
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
        AnimationEnded?.Invoke();
    }
}
