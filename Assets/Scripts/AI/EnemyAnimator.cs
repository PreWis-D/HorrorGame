using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private float _flyPosition = 1f;
    [SerializeField] private float _flyDuration = 2f;
    [SerializeField] private ParticleSystem _flyVFX;

    private Animator _animator;
    private string _currentAnimation;
    private const string IdleAnimation = "Idle";
    private const string WalkAnimation = "Walk";
    private const string FollowingAnimation = "Following";
    private const string PlayerDiedAnimation = "PlayerDied";
    private const string EmotionsAnimation = "EmotionsInt";
    private const int NeutralAnimation = 0;
    private const int SmilingAnimation = 1;

    private Tween _tween;
    public bool IsFly { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        _animator.enabled = true;
    }

    public void Deactivate()
    {
        _animator.SetBool(IdleAnimation, true);
        _animator.enabled = false;
    }

    public void Fly(bool isFly)
    {
        _tween.Kill();

        if (isFly)
        {
            _flyVFX.Play();
            SetPlayerSpottedAnimation();
            _tween = transform.DOLocalMove(new Vector3(0, _flyPosition, 0), _flyDuration).SetEase(Ease.Linear);
        }
        else
        {
            _flyVFX.Stop();
            _tween = transform.DOLocalMove(Vector3.zero, _flyDuration).SetEase(Ease.Linear);
            _tween.OnComplete(SetIdleAnimation);
        }

        IsFly = isFly;
    }

    public void SetWalkAnimation()
    {
        SetBool(_currentAnimation, false);
        SetBool(WalkAnimation, true);
        _currentAnimation = WalkAnimation;
    }

    public void SetIdleAnimation()
    {
        SetBool(_currentAnimation, false);
        SetBool(IdleAnimation, true);
        _currentAnimation = IdleAnimation;
    }

    public void SetPlayerSpottedAnimation()
    {
        SetBool(_currentAnimation, false);
        SetBool(FollowingAnimation, true);
        _currentAnimation = FollowingAnimation;
    }

    public void PlayerDied()
    {
        SetBool(_currentAnimation, false);
        SetBool(PlayerDiedAnimation, true);
        _currentAnimation = PlayerDiedAnimation;
    }

    public void SetEmotion(int value)
    {
        if (value == 0)
            SetInteger(NeutralAnimation);
        else if (value == 1)
            SetInteger(SmilingAnimation);
    }

    private void SetInteger(int value)
    {
        _animator.SetInteger(EmotionsAnimation, value);
    }

    private void SetBool(string animation, bool value)
    {
        _animator.SetBool(animation, value);
    }
}
