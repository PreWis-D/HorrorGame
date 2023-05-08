using UnityEngine;

public struct LevelData
{
    private const string CURRENT_LEVEL_ID = "CurrentLevelID";

    public int CurrentLevel => PlayerPrefs.GetInt(CURRENT_LEVEL_ID, 1);

    public void Save(int value)
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + value);
    }
}
