using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private Collectable _currentCollectable;
    private Color _fadeColor = new Color(1, 1, 1, 0); 
    private Color _showColor = new Color(1, 1, 1, 1);
    
    public void SetItem(Collectable collectable)
    {
        _currentCollectable = collectable;
        _icon.sprite = collectable.Sprite;
        _icon.color = _showColor;
    }

    public Collectable GetItem()
    {
        return _currentCollectable;
    }

    public void RemoveItem()
    {
        _currentCollectable = null;
        _icon.sprite = null;
        _icon.color = _fadeColor;
    }
}