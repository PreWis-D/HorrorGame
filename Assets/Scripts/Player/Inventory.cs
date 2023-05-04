using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Player _player;

    private List<Collectable> _items = new List<Collectable>();
    private int _maxCountItems = 4;

    public List<Collectable> Items => _items;

    public Collectable CurrentCollectable { get; private set; }

    public event UnityAction<Collectable> ItemTaked;
    public event UnityAction<Collectable> ItemRemoved;
    public event UnityAction Sorted;
    public event UnityAction<int> Initialized;

    private void OnEnable()
    {
        _player.ItemPickedUp += OnItemPickedUp;
        _player.ItemThrown += OnItemThrown;
        _player.Mouse0KeyClicked += OnMouse0KeyClicked;
        _player.ItemClicked += OnItemClicked;
    }

    private void OnDisable()
    {
        _player.ItemPickedUp -= OnItemPickedUp;
        _player.ItemThrown -= OnItemThrown;
        _player.Mouse0KeyClicked -= OnMouse0KeyClicked;
        _player.ItemClicked -= OnItemClicked;
    }

    public void Init(int maxCount)
    {
        _maxCountItems = maxCount;
        Initialized?.Invoke(_maxCountItems);
    }

    public void AddItem(Collectable collectable)
    {
        _items.Add(collectable);
        ItemTaked?.Invoke(collectable);
    }

    public void RemoveItem(Collectable collectable)
    {
        _items.Remove(collectable);
        ItemRemoved?.Invoke(collectable);
    }

    public void UnlinkCurrentItem()
    {
        CurrentCollectable = null;
    }

    public void UseItem()
    {
        RemoveItem(CurrentCollectable);
        UnlinkCurrentItem();
        Sort();
    }

    public bool CheckEmptySlot()
    {
        return _items.Count < _maxCountItems;
    }

    private void OnMouse0KeyClicked()
    {
        if (CurrentCollectable)
            CurrentCollectable.Execute();
    }

    private void OnItemPickedUp(Collectable collectable)
    {
        if (_items.Count < _maxCountItems)
        {
            collectable.OnTaked(_player.Hand);

            if (_items.Count < 1)
                CurrentCollectable = collectable;
            else
                collectable.gameObject.SetActive(false);

            AddItem(collectable);
        }
    }

    private void OnItemThrown()
    {
        RemoveItem(CurrentCollectable);
        CurrentCollectable.OnThrow();
        UnlinkCurrentItem();
        Sort();
    }

    private void OnItemClicked(int item)
    {
        int index = 0;

        foreach (var currentItem in _items)
        {
            if (index == item - 1)
            {
                if (CurrentCollectable != currentItem)
                {
                    CurrentCollectable.gameObject.SetActive(false);
                    CurrentCollectable = currentItem;
                    CurrentCollectable.gameObject.SetActive(true);
                }
            }

            index++;
        }
    }

    private void Sort()
    {
        if (_items.Count > 0)
        {
            CurrentCollectable = _items[0];
            CurrentCollectable.gameObject.SetActive(true);
            Sorted?.Invoke();
        }
    }
}