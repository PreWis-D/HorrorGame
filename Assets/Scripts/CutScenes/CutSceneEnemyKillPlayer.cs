using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class CutSceneEnemyKillPlayer : MonoBehaviour
{
    [SerializeField] private float _delayPlayAudio = 0.2f;

    private Player _player;
    private Enemy _enemy;
    private Camera _enemyCamera;
    private Camera _playerCamera;

    private AudioSource _audioSource;

    public event UnityAction CutSceneEnded;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _enemy.PlayerReached += OnPlayerReached;
    }

    private void OnDisable()
    {
        _enemy.PlayerReached -= OnPlayerReached;
    }

    public void Init(Player player, Enemy enemy)
    {
        _player = player;
        _enemy = enemy;
        _playerCamera = _player.GetComponentInChildren<Camera>();
        _enemyCamera = _enemy.Camera;
    }

    private void OnPlayerReached()
    {
        _player.Die();
        _playerCamera.gameObject.SetActive(false);
        _enemyCamera.gameObject.SetActive(true);
        StartAnimation();
    }

    private void StartAnimation()
    {
        _enemy.KillPlayer();

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);

        _enemy.SetEmotion(1);
        _enemyCamera.GetComponent<Animator>().SetTrigger("Die");

        yield return new WaitForSeconds(_delayPlayAudio);
        _audioSource.Play();

        yield return new WaitForSeconds(3);

        CutSceneEnded?.Invoke();
    }
}
