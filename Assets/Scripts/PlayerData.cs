using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private float _health = 50f;
    public bool isAlive = true;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health < 0)
        {
            isAlive = false;
            Debug.Log("player is dead");
        }
    }
}
