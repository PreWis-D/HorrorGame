using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingPanel : Panel
{
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _backMainButton;
    [SerializeField] private TMP_Text _audioText;

    private bool _isAudioOn = true;

    public event UnityAction<bool> AudioChanged;
    public event UnityAction BackMainButtonClicked;

    private void OnEnable()
    {
        _soundButton.onClick.AddListener(SoundButtonClick);
        _backMainButton.onClick.AddListener(BackMainButtonClick);
    }

    private void OnDisable()
    {
        _soundButton.onClick.RemoveListener(SoundButtonClick);
        _backMainButton.onClick.RemoveListener(BackMainButtonClick);
    }

    public void SetAudiOn(bool isAudioOn)
    {
        _isAudioOn = isAudioOn;
        ChangeAudioIcon();
    }

    private void SoundButtonClick()
    {
        _isAudioOn = !_isAudioOn;
        ChangeAudioIcon();
        AudioChanged?.Invoke(_isAudioOn);
    }

    private void ChangeAudioIcon()
    {
        if (_isAudioOn)
            _audioText.text = "«вук: вкл";
        else
            _audioText.text = "«вук: выкл";
    }

    private void BackMainButtonClick()
    {
        BackMainButtonClicked?.Invoke();
    }
}
