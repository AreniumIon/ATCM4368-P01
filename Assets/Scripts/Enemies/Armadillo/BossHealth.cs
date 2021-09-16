using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealth : Health
{

    [SerializeField] TextMeshProUGUI healthText;

    protected new int CurrentHealth
    {
        get => base.CurrentHealth;
        set
        {
            base.CurrentHealth = value;
            healthText.text = base.CurrentHealth.ToString();
        }
    }

    protected new void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public override bool TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        TakeDamageFeedback();
        if (CurrentHealth <= 0)
            Kill();
        return true;
    }

    public override void Kill()
    {
        gameObject.SetActive(false);
        // particles
        // sounds
    }

}
