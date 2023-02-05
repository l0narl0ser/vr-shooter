﻿using HurricaneVR.Framework.Components;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Controllers
{
    public class EnemyController : HVRDamageHandlerBase
    {
        private static readonly int Flight = Animator.StringToHash("Flight");
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        private static readonly int Dead = Animator.StringToHash("Dead");
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float health = 50f;
        [SerializeField] private Image healthImage;
        [SerializeField] private Image backImage;
        [SerializeField] private Animator animatorController;
        private UnityEngine.AI.NavMeshAgent agent;
        private PlayerData _player;
        private Transform _cameraTransform;
        private int _deadDistance = 2;
        private float _timerAttack = 0;
        private float currentHeath;


        [Inject]
        public void Construct(PlayerData player)
        {
            _player = player;
        }


        private void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            currentHeath = health;
            animatorController.SetBool(Flight, true);
        }


        private void Update()
        {
            agent.destination = _player.gameObject.transform.position;
            healthImage.transform.LookAt(_cameraTransform);
            backImage.transform.LookAt(_cameraTransform);
            if (Vector3.Distance(_player.gameObject.transform.position, gameObject.transform.position) < _deadDistance)
            {
                _timerAttack -= Time.deltaTime;
                if (_timerAttack <= 0)
                {
                    animatorController.SetTrigger(Shoot);
                    _player.TakeDamage(damage);
                    _timerAttack = timeBetweenAttacks;
                }
            }
        }


        public override void TakeDamage(float damage)
        {
            currentHeath -= damage;
            healthImage.fillAmount = currentHeath / health;
            if (currentHeath <= 0)
            {
                animatorController.SetBool(Dead, true);
                Observable.Timer(TimeSpan.FromSeconds(1.5))
                    .Subscribe(_ => DestroyEnemy())
                    .AddTo(this);
            }
        }


        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }
}