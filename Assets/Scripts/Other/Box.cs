using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject _bossBullet;
    [SerializeField] int _bulletCount;

    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.TakeDamageEvent += CheckTakeDamage;
    }

    public void CheckTakeDamage(int damageAmount, GameObject attacker)
    {
        if (attacker != null)
        {
            if (attacker.GetComponent<Armadillo>() != null || attacker.GetComponent<ArmadilloTail>() != null || attacker.GetComponent<Bullet>() != null)
            {
                ShootBullets();
                health.TakeDamage(3, null);
            }
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
