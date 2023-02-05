using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private Transform startPosition;
    public override void InstallBindings()
    {

    }
}