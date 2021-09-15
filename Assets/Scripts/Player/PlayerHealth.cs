using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : Health
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

    // Stored as an int to prevent duplicate invincibility calls messing up timing
    int _invincibilityStacks = 0;
    protected bool IsInvincible
    { 
        get
        {
            return _invincibilityStacks > 0;
        }
        set
        {
            if (value == true)
            {
                _invincibilityStacks += 1;
            }
            else if (value == false)
            {
                _invincibilityStacks -= 1;
                if (_invincibilityStacks < 0)
                    _invincibilityStacks = 0;
            }
        }
    }

    protected new void Start()
    {
        CurrentHealth = MaxHealth;
        IsInvincible = false;
    }

    public override bool TakeDamage(int amount)
    {
        if (!IsInvincible)
        {
            CurrentHealth -= amount;
            if (CurrentHealth <= 0)
                Kill();
            return true;
        }
        return false;
    }

    public override void Kill()
    {
        gameObject.SetActive(false);
        // particles
        // sounds
    }

    public void MakeInvincible(float time)
    {
        IsInvincible = true;
        StartCoroutine(WaitAndRemoveInvincibility(time));
    }

    protected IEnumerator WaitAndRemoveInvincibility(float time)
    {
        yield return new WaitForSeconds(time);
        IsInvincible = false;
    }
}
