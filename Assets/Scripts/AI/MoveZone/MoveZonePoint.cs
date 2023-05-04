using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class MoveZonePoint : MonoBehaviour
{
    private SphereCollider _collider;
    private Rigidbody _rigidbody;
    private bool _isEmpty = true;
    private Enemy _spawnObject;

    public bool IsEmpty => _isEmpty;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider.isTrigger = true;
        _rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ImpassableTerritory impassableTerritory))
            TakePosition();

        if (other.TryGetComponent(out Enemy enemy))
            TakePosition();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            DropPosition();
    }

    public void TakePosition(Enemy character)
    {
        if (_spawnObject == null)
        {
            _spawnObject = character;
            TakePosition();
        }
    }

    public void TakePosition()
    {
        _isEmpty = false;
    }

    public void DropPosition()
    {
        _isEmpty = true;
    }
}
