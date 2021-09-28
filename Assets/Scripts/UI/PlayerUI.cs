using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Slider slider;

    private void OnEnable()
    {
        playerHealth.HealthChangedEvent += UpdatePlayerHealth;
    }

    private void OnDisable()
    {
        playerHealth.HealthChangedEvent -= UpdatePlayerHealth;
    }

    public void UpdatePlayerHealth(int currentHealth, int maxHealth)
    {
        float healthRatio = 1f * currentHealth / maxHealth;
        slider.value = healthRatio;
    }
}
