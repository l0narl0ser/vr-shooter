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

    public override void InstallBindings()
    {
        Container.InstantiatePrefab(pistolPrefab, startPosition);

        Container.Bind<EnemyController>().FromComponentInNewPrefab(enemyController).AsTransient().Lazy();
        Container.Bind<SpawnPositionService>().FromInstance(spawnPositionService).AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<SpawnService>().AsSingle().NonLazy();
    }
}