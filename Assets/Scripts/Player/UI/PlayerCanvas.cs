using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private IntaractableObjectHandler _interactableHandler;
    [SerializeField] private MessagePanel _messagePanel;
    [SerializeField] private Image _eButtonImage;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _interactableHandler.ObjectRaycastReached += ShowAction;
        _interactableHandler.ObjectRaycastAbandoned += OnObjectAbandoned;
        _interactableHandler.InteractWithObject += OnInteractWithObject;
        _interactableHandler.InventoryFulled += OnInventoryFulled;
    }

    private void OnDisable()
    {
        _interactableHandler.ObjectRaycastReached -= ShowAction;
        _interactableHandler.ObjectRaycastAbandoned -= OnObjectAbandoned;
        _interactableHandler.InteractWithObject -= OnInteractWithObject;
        _interactableHandler.InventoryFulled -= OnInventoryFulled;
    }

    private void ShowAction(string actionDescription)
    {
        _eButtonImage.gameObject.SetActive(true);
        _text.text = actionDescription;
    }

    private void OnInteractWithObject(string description)
    {
        _messagePanel.Show();
        _messagePanel.ShowInfo(description);
    }

    private void OnObjectAbandoned()
    {
        _eButtonImage.gameObject.SetActive(false);
        _messagePanel.ShowInventoryFullText(false);
        _messagePanel.Hide();
        _messagePanel.ShowInfo("");
    }

    private void OnInventoryFulled()
    {
        _messagePanel.ShowInventoryFullText(true);
    }
}
