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
            // Cast because:
            // 1. Don't want to damage player
            // 2. TakeDamage prefers parent classes over child classes, so "Health" gets called over "BossHealth"
            if (damageable as PlayerHealth)
                return;
            else if (damageable as BossHealth)
                ((BossHealth)damageable).TakeDamage(_damage);
            else
                damageable.TakeDamage(_damage);

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
