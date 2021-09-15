using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    [SerializeField] Material invincibilityMaterial;

    List<Material> playerMaterials = new List<Material>();

    protected override void PowerUp(PlayerHealth player)
    {
        player.MakeInvincible(PowerupDuration);

        PlayerVisuals playerVisuals = player.gameObject.GetComponent<PlayerVisuals>();

        // Store player's materials to return later
        foreach (MeshRenderer mr in playerVisuals.meshRenderers)
        {
            playerMaterials.Add(mr.material);
            mr.material = invincibilityMaterial;
        }
    }

    protected override void PowerDown(PlayerHealth player)
    {
        PlayerVisuals playerVisuals = player.gameObject.GetComponent<PlayerVisuals>();
         
        // Return player's materials to corresponding meshes
        int i = 0;
        foreach (MeshRenderer mr in playerVisuals.meshRenderers)
        {
            mr.material = playerMaterials[i++];
        }
    }
}
