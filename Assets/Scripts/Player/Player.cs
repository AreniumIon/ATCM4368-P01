using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TankController), typeof(Inventory))]
public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    public List<MeshRenderer> meshRenderers;

    [SerializeField] int _maxHealth = 3;
    int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            healthText.text = _currentHealth.ToString();
        }
    }

    bool _canTakeDamage;
    public bool CanTakeDamage
    {
        get => _canTakeDamage;
        set => _canTakeDamage = value;
    }

    TankController _tankController;
    Inventory _inventory;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
        _inventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        CurrentHealth = _maxHealth;
        CanTakeDamage = true;
    }

    public void IncreaseHealth(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, _maxHealth);
    }

    // Return true if took damage, false if invincible
    public bool DecreaseHealth(int amount)
    {
        if (CanTakeDamage)
        {
            CurrentHealth -= amount;
            if (_currentHealth <= 0)
                Kill();
            return true;
        }
        return false;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        // particles
        // sounds
    }
}
