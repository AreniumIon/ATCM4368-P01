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
        }
    }

}
