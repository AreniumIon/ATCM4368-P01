using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    // Variables
    [SerializeField] int _maxHealth = 3;
    public int MaxHealth
    {
        get => _maxHealth;
        protected set { _maxHealth = value; HealthChangedEvent.Invoke(CurrentHealth, MaxHealth); }
    }

    private int _currentHealth;
    public int CurrentHealth 
    {
        get => _currentHealth;
        protected set { _currentHealth = value; HealthChangedEvent.Invoke(CurrentHealth, MaxHealth); }
    }

    // Stored as an int to prevent duplicate invincibility calls messing up timing
    int _invincibilityStacks = 0;
    public bool IsInvincible
    {
        get => _invincibilityStacks > 0;
        set
        {
            if (value == true)
                _invincibilityStacks += 1;
            else if (value == false)
                _invincibilityStacks = Mathf.Clamp(_invincibilityStacks - 1, 0, Int32.MaxValue);
        }
    }

    // Events
    // currentHealth, maxHealth
    public event Action<int, int> HealthChangedEvent = delegate { };

    // damageAmount, attacker
    public event Action<int, GameObject> TakeDamageEvent = delegate { };

    //
    public event Action DeathEvent = delegate { };


    // Methods
    protected void Start()
    {
        CurrentHealth = MaxHealth;

        TakeDamageEvent += CheckDeath;

        // Invoke events for initial values
        HealthChangedEvent.Invoke(CurrentHealth, MaxHealth);
    }

    public virtual void IncreaseHealth(int healthAmount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + healthAmount, 0, MaxHealth);
    }


    // Return true if took damage, false if invincible
    public bool TakeDamage(int damageAmount, GameObject attacker)
    {
        if (!IsInvincible)
        {
            CurrentHealth -= damageAmount;
            TakeDamageEvent.Invoke(damageAmount, attacker);
            return true;
        }
        return false;
    }

    private void CheckDeath(int amount, GameObject attacker)
    {
        if (CurrentHealth <= 0)
            Kill();
    }

    public virtual void Kill()
    {
        DeathEvent.Invoke();
        gameObject.SetActive(false);
    }

}
