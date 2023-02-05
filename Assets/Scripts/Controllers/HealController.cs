using Abstractions;
using System;
using UniRx;
using UnityEngine;


namespace Controllers
{
    public class HealController : MonoBehaviour
    {
        [SerializeField] private int _healCount;
        [SerializeField] private int _lifeTime;


        private void Awake()
        {
            Observable.Timer(TimeSpan.FromSeconds(_lifeTime))
                .Subscribe(_ => DestroySelf())
                .AddTo(this);
        }


        private void DestroySelf()
        {
            Debug.Log("HealController is Destroyed");
            Destroy(gameObject);
        }


        private void OnTriggerEnter(Collider other)
        {
            IHealable healable = other.gameObject.GetComponentInChildren<IHealable>();
            if (healable == null)
            {
                return;
            }

            healable.Heal(_healCount);
            DestroySelf();
        }
    }
}