using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.TakeDamageEvent += CheckTakeDamage;
        health.DeathEvent += CheckDeath;
    }

    public void CheckTakeDamage(int damageAmount, GameObject attacker)
    {

    }

    public void CheckDeath()
    {

    }
}
