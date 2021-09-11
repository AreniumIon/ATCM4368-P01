using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth = 3;
    int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
        }
    }


    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public void IncreaseHealth(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, _maxHealth);
    }

    // Return true if took damage, false if invincible
    public bool DecreaseHealth(int amount)
    {
        CurrentHealth -= amount;
        if (_currentHealth <= 0)
            Kill();
        return true;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        // particles
        // sounds
    }
}
