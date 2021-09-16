using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHealth = 3;
    protected int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    private int _currentHealth;
    protected int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }

    protected void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void IncreaseHealth(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
    }

    // Return true if took damage, false if invincible
    public virtual bool TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        TakeDamageFeedback();
        if (CurrentHealth <= 0)
            Kill();
        return true;
    }

    public virtual void Kill()
    {
        gameObject.SetActive(false);
        // particles
        // sounds
    }

    protected virtual void TakeDamageFeedback()
    {
        MeshList meshList = gameObject.GetComponent<MeshList>();
        meshList?.SetMaterial(GameConstants.EnemyDamagedMaterial);
        meshList?.DelayRestoreMaterials(.1f);
    }
}
