using TMPro;
using UnityEngine;

public class MessagePanel : Panel
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _InventoryFullText;

    public void ShowInfo(string targetText)
    {
        _text.text = targetText;
    }

    public void ShowInventoryFullText(bool isShowed)
    {
        _InventoryFullText.gameObject.SetActive(isShowed);
    }
}
