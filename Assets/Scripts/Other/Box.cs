using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject _bossBullet;
    [SerializeField] int _bulletCount;
    LayerMask _spawnBulletsAttackers;

    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        _spawnBulletsAttackers = LayerMask.GetMask("Armadillo", "ArmadilloTail", "BossBullet");
        health.TakeDamageEvent += CheckTakeDamage;
    }

    public void CheckTakeDamage(int damageAmount, GameObject attacker)
    {
        if (attacker != null && MathFunctions.IsMatchingLayer(_spawnBulletsAttackers, attacker.layer))
        {
            ShootBullets();
            health.TakeDamage(3, null);
        }
    }

    private void ShootBullets()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            ShootBullet(i * 360 / _bulletCount);
        }
    }

    private void ShootBullet(float angle)
    {
        Instantiate(_bossBullet, transform.position, Quaternion.Euler(0f, angle, 0f));
    }
}
