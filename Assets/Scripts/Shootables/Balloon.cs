using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Balloon : MonoBehaviour, IShootable, IKillable
{
    [SerializeField] private float _health = 1;

    public void TakeShot(Vector3 hitPosition, float damage)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damageAmount)
    {
        _health -= damageAmount;
        if (!(_health <= 0)) return;
        Destroy(gameObject);
    }
}