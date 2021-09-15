using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : ProjectileBase
{
    [SerializeField] int _damage = 1;

    protected override void Collide(GameObject collision)
    {
        // Deal damage if enemy has health
        BossHealth bossHealth = collision.GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(_damage);
        }

        // Collide for any enemy
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            base.Collide(collision);
        }
    }
}
