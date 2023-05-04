using UnityEngine;

public class Flashlight : Collectable
{
    [SerializeField] private Transform _light;

    private AudioSource _audioSource;
    private bool _isLightActive = false;

    private void Awake()
    {
        _audioSource= GetComponent<AudioSource>();
    }

    public override void Execute()
    {
        base.Execute();

        _isLightActive = !_isLightActive;

        _audioSource.Play();
        if (_isLightActive)
            ActivateLight();
        else
            DeactivateLight();
    }

    private void ActivateLight()
    {
        _light.gameObject.SetActive(true);
        _isLightActive = true;
    }

    private void DeactivateLight()
    {
        _light.gameObject.SetActive(false);
        _isLightActive = false;
    }
}
