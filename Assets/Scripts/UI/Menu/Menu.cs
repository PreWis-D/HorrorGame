using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private SettingDifficultyData _settingDifficultyData;
    [SerializeField] private LevelData _levelData;

    [Header("Panels")]
    [SerializeField] private CanvasFader _mainPanel;
    [SerializeField] private SettingPanel _settingPanel;
    [SerializeField] private ChooseDifficultyPanel _chooseDifficultyPanel;

    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;

    private void Awake()
    {
        if (Time.deltaTime < 1)
            Time.timeScale = 1.0f;
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(PlayButtonClick);
        _settingButton.onClick.AddListener(SettingButtonClick);
        _settingPanel.AudioChanged += OnAudioChanged;
        _settingPanel.BackMainButtonClicked += OnBackMainButtonClick;
        _chooseDifficultyPanel.DifficultyChoised += OnDifficultyChoised;

        _audioManager.AudioListenerState += OnAudioListenerState;
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(PlayButtonClick);
        _settingButton.onClick.RemoveListener(SettingButtonClick);
        _settingPanel.AudioChanged -= OnAudioChanged;
        _settingPanel.BackMainButtonClicked -= OnBackMainButtonClick;
        _chooseDifficultyPanel.DifficultyChoised -= OnDifficultyChoised;

        _audioManager.AudioListenerState -= OnAudioListenerState;
    }

    private void PlayButtonClick()
    {
        _mainPanel.Hide();
        _chooseDifficultyPanel.Show();
    }

    private void SettingButtonClick()
    {
        _mainPanel.Hide();
        _settingPanel.Show();
    }

    private void OnAudioChanged(bool isAudioOn)
    {
        _audioManager.ChangeListenerState(isAudioOn);
    }

    private void OnBackMainButtonClick()
    {
        _settingPanel.Hide();
        _mainPanel.Show();
    }

    private void OnDifficultyChoised(int difficultyIndex)
    {
        _settingDifficultyData.Save(difficultyIndex);
        _sceneChanger.LoadLevel(_levelData.CurrentLevel);
    }

    private void OnAudioListenerState(bool isAudioOn)
    {
        _settingPanel.SetAudiOn(isAudioOn);
    }
}