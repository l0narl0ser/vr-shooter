using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPosition;
    public override void InstallBindings()
    {
        PlayerData player = Container.InstantiatePrefabForComponent<PlayerData>(playerPrefab, startPosition.position, Quaternion.identity, null);
        Container.Bind<PlayerData>().FromInstance(player).AsSingle();
    }
}