using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private CanvasFader _mainPanel;
    [SerializeField] private SettingPanel _settingPanel;
    [SerializeField] private ChooseDifficultyPanel _chooseDifficultyPanel;

    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(PlayButtonClick);
        _settingButton.onClick.AddListener(SettingButtonClick);
        _settingPanel.AudioChanged += OnAudioChanged;
        _settingPanel.BackMainButtonClicked += OnBackMainButtonClick;
        _chooseDifficultyPanel.DifficultyChoised += OnDifficultyChoised;

        _level.AudioStateChanged += OnAudioListenerState;
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(PlayButtonClick);
        _settingButton.onClick.RemoveListener(SettingButtonClick);
        _settingPanel.AudioChanged -= OnAudioChanged;
        _settingPanel.BackMainButtonClicked -= OnBackMainButtonClick;
        _chooseDifficultyPanel.DifficultyChoised -= OnDifficultyChoised;

        _level.AudioStateChanged -= OnAudioListenerState;
    }

    private void Start()
    {
        if (Time.timeScale< 1.0f)
            Time.timeScale = 1.0f;
    }

    public void SetAudiOn(bool isAudioOn)
    {
        _settingPanel.SetAudiOn(isAudioOn);
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
        _level.ChangeAudioState(isAudioOn);
    }

    private void OnBackMainButtonClick()
    {
        _settingPanel.Hide();
        _mainPanel.Show();
    }

    private void OnDifficultyChoised(int difficultyIndex)
    {
        _level.StartGame(difficultyIndex);
    }

    private void OnAudioListenerState(bool value)
    {
        SetAudiOn(value);
    }
}