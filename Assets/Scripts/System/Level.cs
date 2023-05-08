using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

public class Level : MonoBehaviour
{
    [SerializeField] private StartLosePanel _startLosePanel;
    [SerializeField] private CutSceneEnemyKillPlayer _cutSceneEnemyKillPlayer;
    [SerializeField] private AudioManager _audioManager;

    private Player _player;
    private Enemy _enemy;
    private const float _delay = 1;
    private SceneChanger _sceneChanger;
    private LevelData _levelData;

    public event UnityAction<bool> AudioStateChanged;

    [Inject]
    private void Construct(Player player, Enemy enemy)
    {
        _player = player;
        _enemy = enemy;

        _cutSceneEnemyKillPlayer.Init(_player, _enemy);
    }

    private void Awake()
    {
        _sceneChanger = GetComponent<SceneChanger>();
    }

    private void OnEnable()
    {
        _cutSceneEnemyKillPlayer.CutSceneEnded += OnCutSceneEnded;
        _player.Wined += OnWined;

        _audioManager.AudioListenerState += OnAudioListenerState;
    }

    private void OnDisable()
    {
        _cutSceneEnemyKillPlayer.CutSceneEnded -= OnCutSceneEnded;
        _player.Wined -= OnWined;

        _audioManager.AudioListenerState -= OnAudioListenerState;
    }

    private void Start()
    {
        _startLosePanel.Hide();
    }

    public void ExitToMenu()
    {
        _sceneChanger.LoadMenuScene();
    }

    private void OnCutSceneEnded()
    {
        _startLosePanel.Show();
        _enemy.Deactivate();
        _player.Deactivate();
        StartCoroutine(DelayDie());
    }

    private void OnWined()
    {
        _enemy.Deactivate();
        StartCoroutine(DelayWin());
    }

    private IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(_delay);

        _sceneChanger.RestartLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(_delay * 2);

        _startLosePanel.Show();

        yield return new WaitForSeconds(_delay);

        _levelData.Save(1);
        _sceneChanger.LoadLevel(_levelData.CurrentLevel);
    }

    #region AudioManager
    public void ChangeAudioState(bool isListenerOn)
    {
        _audioManager.ChangeListenerState(isListenerOn);
    }

    public void ChangeVolume(bool isAudioOn)
    {
        _audioManager.ChangeVolume(isAudioOn);
    }

    private void OnAudioListenerState(bool value)
    {
        AudioStateChanged?.Invoke(value);
    }
    #endregion
}
