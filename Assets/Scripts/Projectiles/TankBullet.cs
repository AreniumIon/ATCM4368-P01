using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : ProjectileBase
{
    [SerializeField] int _damage = 1;

    protected override void Collide(GameObject collision)
    {
        // Deal damage if enemy has health
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.DecreaseHealth(_damage);
        }

        // Collide for any enemy
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            base.Collide(collision);
        }
    }
}
