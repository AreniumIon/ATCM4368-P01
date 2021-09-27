using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    [SerializeField] Material invincibilityMaterial;

    protected override void PowerUp(Health player)
    {
        player.IsInvincible = true;

        MeshList playerMeshList = player.gameObject.GetComponent<MeshList>();
        playerMeshList?.SetMaterial(invincibilityMaterial);
    }

    protected override void PowerDown(Health player)
    {
        player.IsInvincible = false;

        MeshList playerMeshList = player.gameObject.GetComponent<MeshList>();
        playerMeshList?.RestoreMaterials();
         
    }
}
