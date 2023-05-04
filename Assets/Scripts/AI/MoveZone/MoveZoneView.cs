using UnityEngine;

public class MoveZoneView : MonoBehaviour
{
    [SerializeField] private int _step;
    [SerializeField] private Vector2Int _moveZoneSize;
    [SerializeField] private MoveZone _moveZone;

    private int _minValue = 1;
    private Vector2Int _currentSize;

    public MoveZone MoveZone => _moveZone;

    private void OnValidate()
    {
        if (_step <= 0)
            _step = _minValue;
    }

    private void Awake()
    {
        _currentSize = _moveZoneSize;
        Init();
    }

    private void Init()
    {
        _moveZone.Initialize(_moveZoneSize, _step);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 1, 0.1f);
        Gizmos.DrawCube(_moveZone.transform.position, new Vector3(_moveZoneSize.x, 0.5f, _moveZoneSize.y));
    }
}
