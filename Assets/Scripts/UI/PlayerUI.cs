using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Health _playerHealth;
    [SerializeField] HealthBar _healthBar;
    [SerializeField] TextMeshProUGUI _healthText;

    private void OnEnable()
    {
        _playerHealth.HealthChangedEvent += UpdatePlayerHealth;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChangedEvent -= UpdatePlayerHealth;
    }

    public void UpdatePlayerHealth(int currentHealth, int maxHealth)
    {
        float healthRatio = 1f * currentHealth / maxHealth;
        _healthBar.SetValue(healthRatio);
        _healthText.text = currentHealth + "/" + maxHealth;
    }
}
