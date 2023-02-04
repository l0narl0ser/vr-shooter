using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


namespace Service
{
    public class SpawnService
    {
        private const int MaxCountSpawnEnemy= 5;
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
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ => CheckAllEnemyKilled());
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
            List<int> usedSpawnedPoint = new List<int>();
            for (int i = 0; i < MaxCountSpawnEnemy; i++)
            {
                EnemyController enemyController = _diContainer.Resolve<EnemyController>();
                _enemys.Add(enemyController);
                int randomPoint = Random.Range(0, _spawnPositionService.SpawnPoints.Count - 1);
                usedSpawnedPoint.Add(randomPoint);
                Transform spawnPoint = _spawnPositionService.SpawnPoints[randomPoint];
                enemyController.transform.position = spawnPoint.position;
            }
        }
    }
}