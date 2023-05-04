using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(SphereCollider))]
public class SpawnPoint : MonoBehaviour
{
    private SphereCollider _collider;
    private bool _isEmpty = true;
    private Character _spawnObject;

    public bool IsEmpty => _isEmpty;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
    }

    public void TakePosition(Character character)
    {
        if (_spawnObject == null)
            _spawnObject = character;
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
