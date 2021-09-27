using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject _bossBullet;
    [SerializeField] int _bulletCount;
    Health health;

    // If attacked by these layers, instantly die
    LayerMask _instantDeathLayers;

    // If killed by these layers, shoot bullets
    LayerMask _shootBulletsLayers;

    // If killed by these layers, maybe spawn health
    LayerMask _spawnHealthLayers;


    private void Start()
    {
        health = GetComponent<Health>();
        _shootBulletsLayers = LayerMask.GetMask("Armadillo", "ArmadilloTail", "BossBullet");
        _instantDeathLayers = LayerMask.GetMask("Armadillo", "ArmadilloTail");
        health.TakeDamageEvent += CheckTakeDamage;

    }

    public void CheckTakeDamage(int damageAmount, GameObject attacker)
    {
        if (attacker != null)
        {
            if (MathFunctions.IsMatchingLayer(_shootBulletsLayers, attacker.layer) && health.CurrentHealth <= 0)
                ShootBullets();

            if (MathFunctions.IsMatchingLayer(_instantDeathLayers, attacker.layer) && health.CurrentHealth > 0)
                health.TakeDamage(3, attacker);
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
