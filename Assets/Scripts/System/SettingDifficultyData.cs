using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class SettingDifficultyData
{
    [SerializeField] private SettingDifficulty[] _settingDifficulties;

    public SettingDifficulty[] SettingDifficulties => _settingDifficulties;
}
