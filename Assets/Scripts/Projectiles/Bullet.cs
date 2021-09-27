using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MathFunctions;

public class Bullet : ProjectileBase
{
    [SerializeField] int _damage = 1;
    [SerializeField] LayerMask layerMask;

    protected override void Collide(GameObject collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && IsMatchingLayer(layerMask, collision.layer))
        {
            damageable.TakeDamage(_damage);

            base.Collide(collision);

            /*
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
            */
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
