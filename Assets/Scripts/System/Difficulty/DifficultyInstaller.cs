using UnityEngine;
using Zenject;

public class DifficultyInstaller : MonoBehaviour
{
    [SerializeField] private SettingDifficultyData _settingDifficultyData;

    private Player _player;
    private Light _directionalLight;
    private Enemy _enemy;

    private int _currentDifficultyIndex;
    private int _easyDifficultyIndex = 0;
    private int _normalDifficultyIndex = 1;
    private int _hardDifficultyIndex = 2;

    [Inject]
    private void Construct(Player player, Light light, Enemy enemy)
    {
        _player = player;
        _directionalLight = light;
        _enemy = enemy;
        _currentDifficultyIndex = _settingDifficultyData.Load();

        TrySetDifficulty(_currentDifficultyIndex);
    }

    public void Init(int difficultyIndex, Player player, Light light, Enemy enemy)
    {
        _currentDifficultyIndex = difficultyIndex;
        _player = player;
        _directionalLight = light;
        _enemy = enemy;

        TrySetDifficulty(_currentDifficultyIndex);
    }

    private void TrySetDifficulty(int currentDifficultyIndex)
    {
        if (currentDifficultyIndex == _easyDifficultyIndex)
            SetDifficultyParametrs(_easyDifficultyIndex);
        else if (currentDifficultyIndex == _normalDifficultyIndex)
            SetDifficultyParametrs(_normalDifficultyIndex);
        else if (currentDifficultyIndex == _hardDifficultyIndex)
            SetDifficultyParametrs(_hardDifficultyIndex);
    }

    private void SetDifficultyParametrs(int index)
    {
        _directionalLight.intensity = _settingDifficultyData.SettingDifficulties[index].Light;
        _player.Init(_settingDifficultyData.SettingDifficulties[index].InventoryMaxCount);
        _enemy.Init(_settingDifficultyData.SettingDifficulties[index].EnemyWalkSpeed,
            _settingDifficultyData.SettingDifficulties[index].EnemyRunSpeed,
            _settingDifficultyData.SettingDifficulties[index].EnemyDelay,
            _settingDifficultyData.SettingDifficulties[index].EnemyWaitTime,
            _settingDifficultyData.SettingDifficulties[index].EnemyFollowTime,
            _player);
    }
}