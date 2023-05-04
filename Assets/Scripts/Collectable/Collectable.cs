using UnityEngine;

public class Collectable : Intaractable
{
    [SerializeField] private Vector3 _targetPositionInHand;
    [SerializeField] private Vector3 _targetRotationInHand;
    [SerializeField] protected Rigidbody Rigidbody;
    [SerializeField] protected int ThrowPower = 0;
    [SerializeField] private Sprite _sprite;

    public Sprite Sprite => _sprite;
    public bool CanTaked { get; private set; } = true;

    public void OnTaked(Transform parent)
    {
        transform.position = parent.transform.position;
        transform.parent = parent.transform;
        transform.SetLocalPositionAndRotation(_targetPositionInHand, Quaternion.Euler(_targetRotationInHand));
        Rigidbody.isKinematic = true;
        Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    public void OnThrow()
    {
        transform.parent = null;
        Rigidbody.isKinematic = false;
        Rigidbody.velocity = transform.forward * ThrowPower;
        Rigidbody.constraints = RigidbodyConstraints.None;
        Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public virtual void ChangeTakedState(bool value)
    {
        CanTaked = value;
    }
}