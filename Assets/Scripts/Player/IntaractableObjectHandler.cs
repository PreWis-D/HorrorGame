using UnityEngine;
using UnityEngine.Events;

public class IntaractableObjectHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _animColliderMask;
    [SerializeField] private float _takeDistance;
    [SerializeField] private Player _player;

    private RaycastHit _hit;
    private Intaractable _interactableObject;
    private Collectable _collectableObject;

    public event UnityAction<string> ObjectRaycastReached;
    public event UnityAction<string> InteractWithObject;
    public event UnityAction ObjectRaycastAbandoned;
    public event UnityAction<Collectable> ItemPickedUp;
    public event UnityAction InventoryFulled;

    private void OnDisable()
    {
        _player.PlayerInput.TakeKeyClicked -= OnTakeClicked;
    }

    private void Start()
    {
        _player.PlayerInput.TakeKeyClicked += OnTakeClicked;
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out _hit, _takeDistance, _animColliderMask))
        {
            if (_hit.transform)
                TryGetComponent(_hit);
        }
        else
        {
            TryUnLinkObjects();
        }
    }

    private void OnTakeClicked()
    {
        if (_interactableObject)
        {
            if (_interactableObject.TryGetComponent(out Collectable collectable) && collectable.CanTaked)
            {
                if (_player.Inventory.CheckEmptySlot())
                    ItemPickedUp?.Invoke(collectable);
                else
                    InventoryFulled?.Invoke();
            }

            if (_interactableObject.TryGetComponent(out KeyHole keyHole))
            {
                if (_player.Inventory.CurrentCollectable != null && keyHole.Key == _player.Inventory.CurrentCollectable)
                {
                    _player.Inventory.UseItem();
                    _interactableObject.Execute();
                }
                else
                    InteractWithObject?.Invoke(_interactableObject.Description);
            }
        }
    }

    private void TryGetComponent(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Intaractable targetObject))
        {
            if (_interactableObject == null)
            {
                _interactableObject = targetObject.transform.GetComponent<Intaractable>();
                _collectableObject = _interactableObject.transform.GetComponent<Collectable>();

                if (_interactableObject.IsApplied == false)
                    ObjectRaycastReached?.Invoke(_interactableObject.ActionDescription);

                if (_collectableObject != null && _collectableObject.CanTaked)
                {
                    ObjectRaycastReached?.Invoke(_interactableObject.ActionDescription);
                    InteractWithObject?.Invoke(_interactableObject.Description);
                }
            }
        }
    }

    private void TryUnLinkObjects()
    {
        if (_interactableObject)
        {
            _interactableObject = null;
            ObjectRaycastAbandoned?.Invoke();
        }

        if (_collectableObject)
            _collectableObject = null;
    }
}