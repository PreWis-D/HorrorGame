using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover), typeof(EnemyStateMachine), typeof(EnemyCollisionHandler))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 _searchRadius;
    [SerializeField] private AudioClip _audioClipElectro;
    [SerializeField] private AudioClip _audioClipWalk;
    [SerializeField] private AudioClip[] _laughterAudioClips;

    private EnemyMover _enemyMover;
    private EnemyStateMachine _stateMachine;
    private EnemyAnimator _enemyAnimator;
    private EnemyCollisionHandler _collisionHandler;
    private Transform _targetPosition;
    private Player _player;
    private AudioSource _audioSource;
    private bool _isElectricPlaying;

    public Player Player => _player;
    public EnemyMover EnemyMover => _enemyMover;
    public Vector3 SearchRadius => _searchRadius;
    public Transform TargetPosition => _targetPosition;
    public bool IsFly => _enemyAnimator.IsFly;

    public float FollowTime { get; private set; } = 4f;
    public float WaitingTime { get; private set; } = 0.5f;
    public float Delay { get; private set; } = 1.5f;
    public FieldOfView FieldOfView { get; private set; }
    public bool IsPlayerReached { get; private set; }
    public MoveZonePoint MoveZonePoint { get; private set; }

    public event UnityAction PlayerReached;
    public event UnityAction PlayerSpotted;
    public event UnityAction PlayerMissed;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _stateMachine = GetComponent<EnemyStateMachine>();
        _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
        _audioSource = GetComponent<AudioSource>();
        FieldOfView = GetComponent<FieldOfView>();
    }

    private void OnEnable()
    {
        _collisionHandler.PlayerCollisionEnter += OnPlayerCollisionEnter;
        _collisionHandler.PlayerCollisionExit += OnPlayerCollisionExit;
        FieldOfView.PlayerSpotted += OnPlayerSpotted;
        FieldOfView.PlayerMissed += OnPlayerMissed;
    }

    private void OnDisable()
    {
        _collisionHandler.PlayerCollisionEnter -= OnPlayerCollisionEnter;
        _collisionHandler.PlayerCollisionExit -= OnPlayerCollisionExit;
        FieldOfView.PlayerSpotted -= OnPlayerSpotted;
        FieldOfView.PlayerMissed -= OnPlayerMissed;
    }

    private void Start()
    {
        StartCoroutine(Laugh());
        StartCoroutine(CheckPlayerInRadius());
    }

    public void Init(float walkSpeed, float runSpeed, float delay, float waitingTime, float followTime)
    {
        Delay = delay;
        WaitingTime = waitingTime;
        FollowTime = followTime;
        _enemyMover.Init(walkSpeed, runSpeed);
    }

    public void Activate()
    {
        _collisionHandler.Activate();
        _stateMachine.Activate();
        _enemyAnimator.Activate();
        _enemyMover.Activate();
    }

    public void Deactivate()
    {
        _collisionHandler.Deactivate();
        _stateMachine.Deactivate();
        _enemyAnimator.Deactivate();
        _enemyMover.Deactivate();
        if (_audioSource.isPlaying)
            _audioSource.Stop();
    }

    public Transform CheckTargetPosition()
    {
        return _targetPosition;
    }

    public void LinkSpawnPoint(MoveZonePoint moveZonePoint)
    {
        UnlinkSpawnPoint();

        MoveZonePoint = moveZonePoint;
        MoveZonePoint.TakePosition(this);
    }

    public void UnlinkSpawnPoint()
    {
        if (MoveZonePoint != null)
        {
            MoveZonePoint.DropPosition();
            MoveZonePoint = null;
        }
    }

    public void SetTargetPosition(Transform target)
    {
        _targetPosition = target;
    }

    public void DropTargetPosition()
    {
        _targetPosition = null;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public bool TryMoveTargetPosition()
    {
        if (_targetPosition != null && _enemyMover.CheckNavMesh())
        {
            _enemyMover.SetWalkSpeed();
            _enemyMover.Move(_targetPosition);
            ChangeAudioElectro(true, _audioClipWalk);
            _enemyAnimator.SetWalkAnimation();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryFollowPlayer()
    {
        if (_player != null)
        {
            _enemyMover.SetRunSpeed();
            _enemyMover.Move(_player.transform);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StopMove()
    {
        _enemyMover.Stop();
        ChangeAudioElectro(false, _audioClipWalk);
        DropTargetPosition();
        _enemyAnimator.SetIdleAnimation();
    }

    public void ChangePlayerSpottedAnimation(bool isSpotted)
    {
        if (isSpotted)
        {
            transform.LookAt(_player.transform);
            _enemyAnimator.Fly(true);
            ChangeAudioElectro(true, _audioClipElectro);
        }
        else
        {
            _enemyAnimator.Fly(false);
            ChangeAudioElectro(false, _audioClipElectro);
        }
    }

    public void KillPlayer()
    {
        _enemyAnimator.PlayerDied();
    }

    public void SetEmotion(int value)
    {
        _enemyAnimator.SetEmotion(value);
    }

    private void ChangeAudioElectro(bool value, AudioClip audioClip)
    {
        if (_audioSource.maxDistance > 20)
            _audioSource.maxDistance = 20;

        if (value)
        {
            _audioSource.loop = true;
            Debug.Log(audioClip);
            _audioSource.PlayOneShot(audioClip);
        }    
        else
        {
            _audioSource.Stop();
            _audioSource.loop = false;
        }

        _isElectricPlaying = value;
    }

    private void OnPlayerCollisionEnter(Player player)
    {
        IsPlayerReached = true;
        SetPlayer(player);
        PlayerReached?.Invoke();
    }

    private void OnPlayerCollisionExit()
    {
        IsPlayerReached = false;
        SetPlayer(null);
    }

    private IEnumerator CheckPlayerInRadius()
    {
        WaitForSeconds cooldawn = new WaitForSeconds(1);

        while (true)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, SearchRadius / 2, Quaternion.identity, FieldOfView.PlayerMask);

            if (colliders.Length > 0)
            {
                Player player = colliders[0].GetComponent<Player>();

                if (player)
                    transform.LookAt(player.transform);
            }

            yield return cooldawn;
        }
    }

    private void OnPlayerSpotted(Player player)
    {
        SetPlayer(player);
        PlayerSpotted?.Invoke();
    }

    private void OnPlayerMissed()
    {
        SetPlayer(null);
        PlayerMissed?.Invoke();
    }

    private IEnumerator Laugh()
    {
        while (IsPlayerReached == false)
        {
            int random = Random.Range(15, 25);
            yield return new WaitForSeconds((float)random);

            if (_isElectricPlaying == false)
                PlayLaughterSound();

            yield return new WaitForSeconds(2.1f);

            if (_isElectricPlaying == false)
                _audioSource.Stop();
        }
    }

    private void PlayLaughterSound()
    {
        int random = Random.Range(0, _laughterAudioClips.Length);
        _audioSource.maxDistance = 100;
        _audioSource.PlayOneShot(_laughterAudioClips[random]);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.2f);
        Gizmos.DrawCube(transform.position, _searchRadius);
    }
}