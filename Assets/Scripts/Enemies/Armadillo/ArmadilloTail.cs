using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilloTail : MonoBehaviour, IDamageable
{
    // Attached to Armadillo's Tail. Allows tail collider to call code from base Armadillo

    [SerializeField] Armadillo _armadillo;

    private void OnCollisionEnter(Collision collision)
    {
        _armadillo.DoCollision(collision.gameObject);
    }

    public bool TakeDamage(int damage)
    {
        return _armadillo.GetComponent<Health>().TakeDamage(damage);
    }
}
