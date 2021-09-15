using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : ProjectileBase
{
    [SerializeField] int _damage = 1;

    protected override void Collide(GameObject collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Cast before dealing damage
            if (damageable as BossHealth)
                ((BossHealth)damageable).TakeDamage(_damage);
            else if (damageable as Health)
                ((Health)damageable).TakeDamage(_damage);

        }

        // Collide for any enemy
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            base.Collide(collision);
        }
    }
}

// This code prefers Base Health class over child classes like BossHealth, so it wont work
/*
        // Deal damage if enemy has health
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }
*/
