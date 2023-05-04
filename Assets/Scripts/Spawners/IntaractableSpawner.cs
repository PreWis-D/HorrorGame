using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class IntaractableSpawner : MonoBehaviour
{
    [SerializeField] private Intaractable[] _prefabs;
    [SerializeField] private Transform _spawnPointsContainer;
    [SerializeField] private float _cooldown;

    private SpawnPoint[] _spawnPoints;
    private WaitForSeconds _waitForSeconds;
    private Coroutine _spawnInJob;

    public event UnityAction<Intaractable> Spawned;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_cooldown);
        _spawnPoints = new SpawnPoint[_spawnPointsContainer.childCount];
        for (int i = 0; i < _spawnPoints.Length; i++)
            _spawnPoints[i] = _spawnPointsContainer.GetChild(i).GetComponent<SpawnPoint>();        
    }

    private void Start()
    {
        _spawnInJob = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _prefabs.Length; i++)
        {
            yield return _waitForSeconds;

            int randomNumber = Random.Range(0, _spawnPoints.Length);

            if (_spawnPoints[randomNumber].IsEmpty)
            {
                Intaractable intaractable = Instantiate(_prefabs[i], _spawnPoints[randomNumber].transform.position, Quaternion.identity);
                intaractable.OnSpawned();
                _spawnPoints[randomNumber].TakePosition();
                Spawned?.Invoke(intaractable);
            }
            else
            {
                i--;
            }
        }
    }
}
