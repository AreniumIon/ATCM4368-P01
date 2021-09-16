using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    [SerializeField] Material invincibilityMaterial;

    protected override void PowerUp(PlayerHealth player)
    {
        player.MakeInvincible(PowerupDuration);

        MeshList playerMeshList = player.gameObject.GetComponent<MeshList>();
        playerMeshList?.SetMaterial(invincibilityMaterial);
    }

    protected override void PowerDown(PlayerHealth player)
    {
        MeshList playerMeshList = player.gameObject.GetComponent<MeshList>();
        playerMeshList?.RestoreMaterials();
         
    }
}
