using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankGun : MonoBehaviour
{
    [SerializeField] float _fireRate = .5f;
    public float FireRate
    {
        get => _fireRate;
        set => _fireRate = value;
    }
        
    float _fireTimer = 0f;
    private float FireTimer
    {
        get => _fireTimer;
        set => _fireTimer = Mathf.Max(0f, value);
    }

    [SerializeField] Transform _bulletSpawn;

    [SerializeField] GameObject _bulletPrefab;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            Fire();

        FireTimer -= Time.deltaTime;
    }

    public void Fire()
    {
        if (FireTimer <= 0f)
        {
            // Spawn Projectile
            Instantiate(_bulletPrefab, _bulletSpawn.position, _bulletSpawn.rotation);

            // Reset Timer
            FireTimer = FireRate;
        }
    }
}
