using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossUI : MonoBehaviour
{
    [SerializeField] Health bossHealth;
    [SerializeField] TextMeshProUGUI healthText;

    private void OnEnable()
    {
        bossHealth.HealthChangedEvent += UpdateBossHealth;
    }

    private void OnDisable()
    {
        bossHealth.HealthChangedEvent -= UpdateBossHealth;
    }

    public void UpdateBossHealth(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth + "/" + maxHealth;
    }
}
