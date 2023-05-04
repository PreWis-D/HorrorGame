using System.Collections.Generic;
using UnityEngine;

public class MoveZone : MonoBehaviour
{
    [SerializeField] private MoveZonePoint _moveZonePointPrefab;
    [SerializeField] private Transform _moveZonePointsContainer;

    private float _offset = 0.5f;

    private List<MoveZonePoint> _moveZonePoints = new List<MoveZonePoint>();

    public List<MoveZonePoint> MoveZonePoints => _moveZonePoints;

    public void Initialize(Vector2Int size, int step)
    {
        Vector2 offset = new Vector2((size.x - step) * _offset, (size.y - step) * _offset);

        for (int y = 0; y < size.y; y += step)
        {
            for (int x = 0; x < size.x; x += step)
            {
                MoveZonePoint spawnPoint = Instantiate(_moveZonePointPrefab, _moveZonePointsContainer);
                spawnPoint.transform.localPosition = new Vector3(x - offset.x, 0, y - offset.y);
                _moveZonePoints.Add(spawnPoint);
            }
        }
    }
}
