using Controllers;
using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;


namespace Service
{
    public class SpawnService : IDisposable
    {
        private const int MaxCountSpawnEnemy= 5;
        private CompositeDisposable compositeDisposable = new CompositeDisposable();
        private readonly DiContainer _diContainer;
        private readonly SpawnPositionService _spawnPositionService;
        private List<EnemyController> _enemys = new List<EnemyController>();


        public SpawnService(DiContainer diContainer, SpawnPositionService spawnPositionService)
        {
            _diContainer = diContainer;
            _spawnPositionService = spawnPositionService;
            Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe(_ => CreateStartSpawn());
        }


        private void CreateStartSpawn()
        {
            GenerateEnemy();
            Observable.Interval(TimeSpan.FromSeconds(5)).Subscribe(_ => CheckAllEnemyKilled()).AddTo(compositeDisposable);
        }


        private void CheckAllEnemyKilled()
        {
            foreach (EnemyController enemyController in _enemys.ToList())
            {
                if (enemyController == null)
                {
                    _enemys.Remove(enemyController);
                }
            }

            if (_enemys.Count == 0)
            {
                GenerateEnemy();
            }
        }


        private void GenerateEnemy()
        {
            for (int i = 0; i < MaxCountSpawnEnemy; i++)
            {
                EnemyController enemyController = _diContainer.Resolve<EnemyController>();
                _enemys.Add(enemyController);
                int randomPoint = Random.Range(0, _spawnPositionService.SpawnPoints.Count - 1);
                Transform spawnPoint = _spawnPositionService.SpawnPoints[randomPoint];
                enemyController.transform.SetParent(spawnPoint);
                NavMeshHit myNavHit;
                if(NavMesh.SamplePosition(spawnPoint.position, out myNavHit, 0.1f , -1))
                {
                    enemyController.transform.position = myNavHit.position;
                }
                else
                {
                    enemyController.transform.position = spawnPoint.transform.position;
                }

            }
        }


        public void Dispose()
        {
            compositeDisposable?.Dispose();
            compositeDisposable = null;
        }
    }
}