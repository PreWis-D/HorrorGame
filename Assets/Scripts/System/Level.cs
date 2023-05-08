using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private StartLosePanel _startLosePanel;
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private CutSceneEnemyKillPlayer _cutSceneEnemyKillPlayer;
    [SerializeField] private CutScenePlayerDie _cutScenePlayerDie;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Light _directionalLight;
    [SerializeField] private bool _isMenu = false;
    [SerializeField] private SettingDifficultyData _settingDifficultyData;

    private SceneChanger _sceneChanger;
    private bool _isPlayerDied = false;
    private int _easyDifficultyIndex = 0;
    private int _normalDifficultyIndex = 1;
    private int _hardDifficultyIndex = 2;

    private const string CURRENT_LEVEL_ID = "CurrentLevelID";
    private const string CURRENT_DIFFICULTY = "CurrentDifficulty";

    public int CurrentDifficulty => PlayerPrefs.GetInt(CURRENT_DIFFICULTY, 0);
    public int CurrentLevel => PlayerPrefs.GetInt(CURRENT_LEVEL_ID, 2);

    public event UnityAction<bool> AudioStateChanged;

    private void Awake()
    {
        _sceneChanger = GetComponent<SceneChanger>();
        if (_isPlayerDied)
            _isPlayerDied = false;
    }

    private void OnEnable()
    {
        if (_isMenu == false)
        {
            _cutSceneEnemyKillPlayer.CutSceneEnded += OnCutSceneEnded;
            _cutScenePlayerDie.CutSceneEnded += OnCutSceneEnded;
            _player.Wined += OnWined;
        }

        _audioManager.AudioListenerState += OnAudioListenerState;
    }

    private void OnDisable()
    {
        if (_isMenu == false)
        {
            _cutSceneEnemyKillPlayer.CutSceneEnded -= OnCutSceneEnded;
            _cutScenePlayerDie.CutSceneEnded -= OnCutSceneEnded;
            _player.Wined -= OnWined;
        }

        _audioManager.AudioListenerState -= OnAudioListenerState;
    }

    private void Start()
    {
        if (_isMenu == false)
        {
            SetDifficultyParametrs();
            _startLosePanel.Hide();
            if (_enemy != null)
                _enemy.Activate();
            _player.Activate();
        }
    }

    public void StartGame(int difficultyState)
    {
        PlayerPrefs.SetInt(CURRENT_DIFFICULTY, difficultyState);
        _sceneChanger.LoadLevel(CurrentLevel);
    }

    public void ExitToMenu()
    {
        _sceneChanger.LoadMenuScene();
    }

    private void OnCutSceneEnded()
    {
        _isPlayerDied = true;
        _startLosePanel.Show();
        _enemy.Deactivate();
        _player.Deactivate();
        StartCoroutine(Delay());
    }

    private void OnWined()
    {
        if (_enemy != null)
            _enemy.Deactivate();
        StartCoroutine(DelayWin());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);

        if (_isPlayerDied)
            _sceneChanger.RestartLevel(SceneManager.GetActiveScene().buildIndex);
        else
            _sceneChanger.LoadLevel(CurrentLevel);
    }

    private IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(2);

        _startLosePanel.Show();

        yield return new WaitForSeconds(1);

        PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + 1);
        StartCoroutine(Delay());
    }

    private void SetDifficultyParametrs()
    {
        if (CurrentDifficulty == _easyDifficultyIndex)
            GetDifficultyDataParametrs(_easyDifficultyIndex);
        else if (CurrentDifficulty == _normalDifficultyIndex)
            GetDifficultyDataParametrs(_normalDifficultyIndex);
        else if (CurrentDifficulty == _hardDifficultyIndex)
            GetDifficultyDataParametrs(_hardDifficultyIndex);
    }

    private void GetDifficultyDataParametrs(int index)
    {
        _directionalLight.intensity = _settingDifficultyData.SettingDifficulties[index].Light;
        _player.Init(_settingDifficultyData.SettingDifficulties[index].InventoryMaxCount);
        if (_enemy != null)
            _enemy.Init(_settingDifficultyData.SettingDifficulties[index].EnemyWalkSpeed,
                _settingDifficultyData.SettingDifficulties[index].EnemyRunSpeed,
                _settingDifficultyData.SettingDifficulties[index].EnemyDelay,
                _settingDifficultyData.SettingDifficulties[index].EnemyWaitTime,
                _settingDifficultyData.SettingDifficulties[index].EnemyFollowTime);
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
