using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject _bossBulletPrefab;
    [SerializeField] int _bulletCount;

    [SerializeField] GameObject _healthPrefab;
    [SerializeField] float _healthChance;

    Health health;

    // If attacked by these layers, instantly die
    LayerMask _instantDeathLayers;

    // If killed by these layers, shoot bullets
    LayerMask _shootBulletsLayers;
    
    // If killed by these layers, spawn health
    LayerMask _spawnHealthLayers;


    private void Start()
    {
        health = GetComponent<Health>();
        _instantDeathLayers = LayerMask.GetMask("Armadillo", "ArmadilloTail");
        _shootBulletsLayers = LayerMask.GetMask("Armadillo", "ArmadilloTail", "BossBullet");
        _spawnHealthLayers = LayerMask.GetMask("PlayerBullet");
        health.TakeDamageEvent += CheckTakeDamage;

    }

    public void CheckTakeDamage(int damageAmount, GameObject attacker)
    {
        if (attacker != null)
        {
            if (MathFunctions.IsMatchingLayer(_shootBulletsLayers, attacker.layer) && health.CurrentHealth <= 0)
                ShootBullets();

            if (MathFunctions.IsMatchingLayer(_spawnHealthLayers, attacker.layer) && health.CurrentHealth <= 0)
                SpawnHealth();

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
        Instantiate(_bossBulletPrefab, transform.position, Quaternion.Euler(0f, angle, 0f));
    }

    private void SpawnHealth()
    {
        float r = Random.Range(0f, 1f);
        if (r < _healthChance)
            Instantiate(_healthPrefab, transform.position, Quaternion.identity);
    }
}
