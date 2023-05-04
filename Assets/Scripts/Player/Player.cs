using EvolveGames;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private IntaractableObjectHandler _objectHandler;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _hand;
    [SerializeField] private AudioClip[] _sends;

    private PlayerController _playerController;
    private PlayerInput _playerInput;

    public Inventory Inventory => _inventory;
    public Transform Hand => _hand;
    public PlayerController PlayerController => _playerController;
    public PlayerInput PlayerInput => _playerInput;

    public event UnityAction<Collectable> ItemPickedUp;
    public event UnityAction<int> ItemClicked;
    public event UnityAction ItemThrown;
    public event UnityAction Mouse0KeyClicked;
    public event UnityAction Died;
    public event UnityAction Wined;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _objectHandler.ItemPickedUp += OnItemPickUp;
        _playerInput.ThroableKeyClicked += OnThrowKeyClicked;
        _playerInput.LeftMouseButtonKeyClicked += OnMouse0KeyClicked;
        _playerInput.ItemKeyClicked += OnItemKeyClicked;
    }

    private void OnDisable()
    {
        _objectHandler.ItemPickedUp -= OnItemPickUp;
        _playerInput.ThroableKeyClicked -= OnThrowKeyClicked;
        _playerInput.LeftMouseButtonKeyClicked -= OnMouse0KeyClicked;
        _playerInput.ItemKeyClicked -= OnItemKeyClicked;
    }

    public void Init(int inventoryMaxCount)
    {
        _inventory.Init(inventoryMaxCount);
    }

    public void Activate()
    {
        _playerController.enabled = true;
        _playerInput.enabled = true;
    }

    public void Deactivate()
    {
        _playerController.enabled= false;
        _playerInput.enabled= false;
    }

    public void Die()
    {
        Deactivate();
        Died?.Invoke();
    }

    public void Win()
    {
        Deactivate();
        Wined?.Invoke();
    }

    private void OnItemPickUp(Collectable collectable)
    {
        ItemPickedUp?.Invoke(collectable);
    }

    private void OnThrowKeyClicked()
    {
        if (_inventory.CurrentCollectable)
            ItemThrown?.Invoke();
    }

    private void OnMouse0KeyClicked()
    {
        Mouse0KeyClicked?.Invoke();
    }

    private void OnItemKeyClicked(int item)
    {
        ItemClicked?.Invoke(item);
    }
}