using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : Enemy
{
    protected override bool PlayerImpact(GameObject player, IDamageable playerDamageable)
    {
        //base.PlayerImpact(player);
        player.GetComponent<Health>().Kill();
        return true;
    }
}
