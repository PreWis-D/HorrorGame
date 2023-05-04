using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ItemView[] _items;

    private void OnEnable()
    {
        _inventory.ItemTaked += AddItem;
        _inventory.ItemRemoved += RemoveItem;
        _inventory.Sorted += OnSorted;
        _inventory.Initialized += Init;
    }

    private void OnDisable()
    {
        _inventory.ItemTaked -= AddItem;
        _inventory.ItemRemoved -= RemoveItem;
        _inventory.Sorted -= OnSorted;
        _inventory.Initialized -= Init;
    }

    public void Init(int maxCount)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (i > maxCount - 1)
                _items[i].gameObject.SetActive(false);
        }
    }

    private void AddItem(Collectable collectable)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].GetItem() == null)
            {
                _items[i].SetItem(collectable);
                return;
            }
        }
    }

    private void RemoveItem(Collectable collectable)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].GetItem() == collectable)
            {
                _items[i].RemoveItem();
                return;
            }    
        }
    }

    private void OnSorted()
    {
        for (int i = 0; i < _items.Length; i++)
            _items[i].RemoveItem();

        for (int i = 0; i < _inventory.Items.Count; i++)
            _items[i].SetItem(_inventory.Items[i]);
    }
}