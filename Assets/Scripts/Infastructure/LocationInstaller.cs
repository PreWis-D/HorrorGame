using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private Transform _playerStartPoint;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _enemyStartPoint;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Light _light;

    public override void InstallBindings()
    {
        BindInstallerInterfaces();
        BindLight();
        BindPlayer();
        BindEnemy();
    }

    private void BindPlayer()
    {
        Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerStartPoint.position, _playerStartPoint.rotation, null);

        Container.Bind<Player>().FromInstance(player).AsSingle();
    }

    private void BindEnemy()
    {
        Enemy enemy = Container.InstantiatePrefabForComponent<Enemy>(_enemyPrefab, _enemyStartPoint.position, _enemyStartPoint.rotation, null);

        Container.Bind<Enemy>().FromInstance(enemy).AsSingle();
    }

    private void BindInstallerInterfaces()
    {
        Container.BindInterfacesAndSelfTo<LocationInstaller>().FromInstance(this).AsSingle();
    }

    private void BindLight()
    {
        Container.BindInterfacesAndSelfTo<Light>().FromInstance(_light).AsSingle();
    }
}
