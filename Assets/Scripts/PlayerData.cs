using Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerData : MonoBehaviour, IHealable
{
    private const float MaxHealth = 150;
    private float _health = MaxHealth;
    public bool isAlive = true;


    public void TakeDamage(float damage)
    {
        if (!isAlive)
        {
            return;
        }
        _health -= damage;
        Debug.Log("player take damage");

        if (_health <= 0)
        {
            isAlive = false;
            Observable.Timer(TimeSpan.FromSeconds(3))
                .Subscribe(_ => ReloadScene())
                .AddTo(this);
            Debug.Log("player is dead");
        }
    }


    private void ReloadScene()
    {
        SceneManager.LoadScene("MainScene");
    }


    public void Heal(int heal)
    {
        Debug.Log($"player Get healed heal {heal}");

        _health = Mathf.Clamp(heal + _health, 0, MaxHealth);
    }
}