using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    // Variables
    [SerializeField] int _maxHealth = 3;
    public int MaxHealth { get => _maxHealth; protected set => _maxHealth = value; }

    private int _currentHealth;
    public int CurrentHealth { get => _currentHealth; protected set => _currentHealth = value; }

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

    // damageAmount
    public event Action<int> TakeDamageEvent = delegate { };

    //
    public event Action DeathEvent = delegate { };


    // Methods
    protected void Start()
    {
        CurrentHealth = MaxHealth;

        TakeDamageEvent += CheckDeath;
    }

    public virtual void IncreaseHealth(int damageAmount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + damageAmount, 0, MaxHealth);
    }


    // Return true if took damage, false if invincible
    public bool TakeDamage(int amount)
    {
        if (!IsInvincible)
        {
            CurrentHealth -= amount;
            TakeDamageEvent.Invoke(amount);
            return true;
        }
        return false;
    }

    private void CheckDeath(int amount)
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
