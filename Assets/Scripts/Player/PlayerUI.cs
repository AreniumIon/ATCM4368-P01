using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] TextMeshProUGUI healthText;

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
        healthText.text = currentHealth + "/" + maxHealth;
    }
}
