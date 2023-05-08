using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private BackMusic _backMusic;

    private const string CURRENT_AUDIO_STATE = "CurrentAudioState";
    private bool _isAudioOn = true;

    public int CurrentAudioState => PlayerPrefs.GetInt(CURRENT_AUDIO_STATE, 1);

    public event UnityAction<bool> AudioListenerState;

    private void Start()
    {
        if (CurrentAudioState == 0)
        {
            ChangeVolume(false);
            AudioListenerState?.Invoke(false);
        }
        else
        {
            AudioListenerState?.Invoke(true);
            ChangeBackMusicState(true);
        }
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        ChangeVolume(!inBackground);
    }

    public void ChangeListenerState(bool isListenerOn)
    {
        if (isListenerOn)
            PlayerPrefs.SetInt(CURRENT_AUDIO_STATE, 1);
        else
            PlayerPrefs.SetInt(CURRENT_AUDIO_STATE, 0);

        ChangeVolume(isListenerOn);
    }

    public void ChangeVolume(bool isAudioOn)
    {
        if (CurrentAudioState == 1)
        {
            _isAudioOn = isAudioOn;

            if (_isAudioOn)
                AudioListener.volume = 1f;
            else
                AudioListener.volume = 0f;
        }
        else
        {
            AudioListener.volume = 0f;
        }
    }

    public void ChangeBackMusicState(bool value)
    {
        if (value)
            _backMusic.Play();
        else
            _backMusic.Stop();
    }
}