using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Health _playerHealth;
    [SerializeField] Slider _slider;
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
        _slider.value = healthRatio;
        _healthText.text = currentHealth + "/" + maxHealth;
    }
}
