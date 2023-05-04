using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySearchPositionState : EnemyState
{
    [SerializeField] private float _cooldown;

    private WaitForSeconds _waitForSeconds;
    private Coroutine _assignInJob;

    public event UnityAction PositionFound;

    private void OnEnable()
    {
        _waitForSeconds = new WaitForSeconds(_cooldown);

        if (Enemy.TargetPosition != null)
            Enemy.UnlinkSpawnPoint();

        Collider[] colliders = Physics.OverlapBox(transform.position, Enemy.SearchRadius / 2);

        AssignPosition(colliders);
    }

    private void OnDisable()
    {
        if (_assignInJob != null)
            StopCoroutine(_assignInJob);
    }

    private void AssignPosition(Collider[] colliders)
    {
        List<MoveZonePoint> spawnPoints = new List<MoveZonePoint>();

        for (int i = 0; i < colliders.Length; i++)
        {
            MoveZonePoint spawnPoint = colliders[i].GetComponent<MoveZonePoint>();

            if (spawnPoint != null)
            {
                spawnPoints.Add(spawnPoint);
            }
        }

        _assignInJob = StartCoroutine(Assign(spawnPoints));
    }

    private IEnumerator Assign(List<MoveZonePoint> spawnPoints)
    {
        while (Enemy.CheckTargetPosition() == false)
        {
            int randomNumber = Random.Range(0, spawnPoints.Count);

            if (spawnPoints[randomNumber].IsEmpty == true)
            {
                Enemy.SetTargetPosition(spawnPoints[randomNumber].transform);
                Enemy.LinkSpawnPoint(spawnPoints[randomNumber]);
            }

            yield return _waitForSeconds;
        }

        PositionFound?.Invoke();
    }
}
