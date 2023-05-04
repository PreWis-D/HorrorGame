using UnityEngine;
using UnityEngine.Events;

public class Intaractable : MonoBehaviour
{
    [SerializeField] private string _description;
    [SerializeField] private string _actionDescription;
    [SerializeField] private Vector3 _spawnedRotation;

    public string Description => _description;
    public string ActionDescription => _actionDescription;

    public bool IsSpawned { get; private set; }
    public bool IsApplied { get; private set; } = false;

    public event UnityAction Executed;

    public virtual void Execute()
    {
        Executed?.Invoke();
    }

    public void OnSpawned()
    {
        transform.rotation = Quaternion.Euler(_spawnedRotation);
        IsSpawned = true;
    }

    protected void ChangeDescription(string description)
    {
        _actionDescription = description;
        _description = description;
    }

    protected void ChangeAppliedState(bool value)
    {
        IsApplied = value;
    }
}