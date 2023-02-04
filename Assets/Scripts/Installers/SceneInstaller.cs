using System.ComponentModel;
using DefaultNamespace;
using HurricaneVR.Framework.Weapons.Guns;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
   
    [SerializeField] private Transform startPosition;
    [SerializeField] private GameObject pistolPrefab;

    public override void InstallBindings()
    {
        Container.InstantiatePrefab(pistolPrefab, startPosition);
    }
}