using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    [SerializeField] Material invincibilityMaterial;

    List<Material> playerMaterials = new List<Material>();

    protected override void PowerUp(Player player)
    {
        player.CanTakeDamage = false;

        // Store player's materials to return later
        foreach (MeshRenderer mr in player.meshRenderers)
        {
            playerMaterials.Add(mr.material);
            mr.material = invincibilityMaterial;
        }
    }

    protected override void PowerDown(Player player)
    {
        player.CanTakeDamage = true;
         
        // Return player's materials to corresponding meshes
        int i = 0;
        foreach (MeshRenderer mr in player.meshRenderers)
        {
            mr.material = playerMaterials[i++];
        }
    }
}
