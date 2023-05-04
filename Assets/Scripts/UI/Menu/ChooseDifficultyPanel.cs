using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseDifficultyPanel : Panel
{
    [SerializeField] private Button _easyPlayButton;
    [SerializeField] private Button _normalPlayButton;
    [SerializeField] private Button _hardPlayButton;
    [SerializeField] private TMP_Text _descriptionText;

    public event UnityAction<int> DifficultyChoised;

    private void OnEnable()
    {
        _easyPlayButton.onClick.AddListener(EasyPlayButtonClick);
        _normalPlayButton.onClick.AddListener(NormalPlayButtonClick);
        _hardPlayButton.onClick.AddListener(HardPlayButtonClick);
    }

    private void OnDisable()
    {
        _easyPlayButton.onClick.RemoveListener(EasyPlayButtonClick);
        _normalPlayButton.onClick.RemoveListener(NormalPlayButtonClick);
        _hardPlayButton.onClick.RemoveListener(HardPlayButtonClick);
    }

    public void EasyButtonEnter()
    {
        string description = "� ����� ������ �������� \n ����� ������ 4 �������� \n ����";
        _descriptionText.color = Color.green;
        ShowDescription(description);
    }

    public void EasyButtonExit()
    {
        HideDescription();
    }

    public void NormalButtonEnter()
    {
        string description = "� ����� ������� �������� \n ����� ������ 3 �������� \n �������";
        _descriptionText.color = Color.yellow;
        ShowDescription(description);
    }

    public void NormalButtonExit()
    {
        HideDescription();
    }

    public void HardButtonEnter()
    {
        string description = "� ����� ������� �������� \n ����� ������ 2 �������� \n ����";
        _descriptionText.color = Color.red;
        ShowDescription(description);
    }

    public void HardButtonExit()
    {
        HideDescription();
    }

    private void EasyPlayButtonClick()
    {
        DifficultyChoised?.Invoke(0);
    }

    private void NormalPlayButtonClick()
    {
        DifficultyChoised?.Invoke(1);
    }

    private void HardPlayButtonClick()
    {
        DifficultyChoised?.Invoke(2);
    }

    private void ShowDescription(string text)
    {
        _descriptionText.text = text;
    }

    private void HideDescription()
    {
        _descriptionText.text = "";
    }
}
