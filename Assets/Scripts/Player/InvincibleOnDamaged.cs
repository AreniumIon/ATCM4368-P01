using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleOnDamaged : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] float _invincibilityTime;

    private void OnEnable()
    {
        health.TakeDamageEvent += MakeInvincible;
    }

    private void OnDisable()
    {
        health.TakeDamageEvent -= MakeInvincible;
    }

    public void MakeInvincible(int damageAmount)
    {
        health.IsInvincible = true;
        StartCoroutine(WaitAndRemoveInvincibility(_invincibilityTime));
    }

    protected IEnumerator WaitAndRemoveInvincibility(float time)
    {
        yield return new WaitForSeconds(time);
        health.IsInvincible = false;
    }
}
