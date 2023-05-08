using UnityEngine;

[System.Serializable]
public class SettingDifficultyData
{
    [SerializeField] private DifficultySettings[] _settingDifficulties;

    private const string CURRENT_DIFFICULTY = "CurrentDifficulty";

    public int CurrentDifficulty => PlayerPrefs.GetInt(CURRENT_DIFFICULTY, 0);
    public DifficultySettings[] SettingDifficulties => _settingDifficulties;

    public void Save(int value)
    {
        Debug.Log(value);
        PlayerPrefs.SetInt(CURRENT_DIFFICULTY, value);
    }

    public int Load()
    {
        return CurrentDifficulty;
    }
}
