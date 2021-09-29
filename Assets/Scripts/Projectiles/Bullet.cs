using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MathFunctions;

public class Bullet : ProjectileBase
{
    [SerializeField] int _damage = 1;
    [SerializeField] LayerMask _layerMask;

    protected override void Collide(GameObject collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && IsMatchingLayer(_layerMask, collision.layer))
        {
            damageable.TakeDamage(_damage, gameObject);

            base.Collide(collision);
        }
    }

}
