using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WinTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _winEffects;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            for (int i = 0; i < _winEffects.Length; i++)
                _winEffects[i].Play();

            _audioSource.Play();
            player.Win();
        }
    }
}