using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilloTail : MonoBehaviour
{
    // Attached to Armadillo's Tail. Allows tail collider to call code from base Armadillo

    [SerializeField] Armadillo _armadillo;

    private void OnCollisionEnter(Collision collision)
    {
        _armadillo.DoCollision(collision);
    }
}
