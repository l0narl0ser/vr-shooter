using Controllers;
using DefaultNamespace;
using Service;
using UnityEngine;
using Zenject;


public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private GameObject pistolPrefab;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private SpawnPositionService spawnPositionService;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPosition;


    public override void InstallBindings()
    {
        GameObject instantiatePrefab = Container.InstantiatePrefab(pistolPrefab);
        instantiatePrefab.transform.position = startPosition.position;


        Container.Bind<EnemyController>().FromComponentInNewPrefab(enemyController).AsTransient().Lazy();
        Container.Bind<SpawnPositionService>().FromInstance(spawnPositionService).AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<SpawnService>().AsSingle().NonLazy();


        PlayerData player = Container.InstantiatePrefabForComponent<PlayerData>(playerPrefab,
            playerSpawnPosition.position, Quaternion.identity, null);
        Container.Bind<PlayerData>().FromInstance(player).AsSingle();
    }
}