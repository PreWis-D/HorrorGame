using EvolveGames;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : Panel
{
    [SerializeField] private GameObject _panel;
    [SerializeField] KeyCode BackKey = KeyCode.Escape;
    [SerializeField] PlayerController _player;
    [SerializeField] private PlayerCanvas _escImagePause;
    [SerializeField] private Button _audioButton;
    [SerializeField] private Button _exitToMenuButton;
    [SerializeField] private Image _markImage;
    [SerializeField] private Level _level;

    private bool _isAudioOn = true;
    private bool _isPaused = false;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = _player.GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _audioButton.onClick.AddListener(CangeAudio);
        _exitToMenuButton.onClick.AddListener(ExitToMenuButtonClick);
        _level.AudioStateChanged += OnAudioListenerState;
    }

    private void OnDisable()
    {
        _audioButton.onClick.RemoveListener(CangeAudio);
        _exitToMenuButton.onClick.RemoveListener(ExitToMenuButtonClick);
        _level.AudioStateChanged -= OnAudioListenerState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(BackKey))
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                _panel.gameObject.SetActive(true);
                _escImagePause.gameObject.SetActive(false);
                _playerInput.enabled = false;
                _player.canMove = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0.0f;
            }
            else
            {
                _panel.gameObject.SetActive(false);
                _escImagePause.gameObject.SetActive(true);
                _playerInput.enabled = true;
                _player.canMove = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1.0f;
            }
        }
    }

    private void CangeAudio()
    {
        _isAudioOn = !_isAudioOn;
        ChangeAudioIcon();
        _level.ChangeAudioState(_isAudioOn);
    }

    private void ChangeAudioIcon()
    {
        _markImage.gameObject.SetActive(_isAudioOn);
    }

    private void OnAudioListenerState(bool value)
    {
        SetAudiOn(value);
    }

    private void SetAudiOn(bool value)
    {
        _isAudioOn = value;
        ChangeAudioIcon();
    }

    private void ExitToMenuButtonClick()
    {
        _level.ExitToMenu();
    }
}
