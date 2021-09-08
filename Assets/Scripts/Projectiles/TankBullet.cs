using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : ProjectileBase
{
    protected override void Collide(GameObject collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            // TODO: decrease enemy health
            Destroy(enemy.gameObject);

            base.Collide(collision);
        }
    }
}
