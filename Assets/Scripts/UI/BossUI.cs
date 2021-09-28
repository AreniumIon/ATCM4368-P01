using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] Health bossHealth;
    [SerializeField] Slider slider;

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
        float healthRatio = 1f * currentHealth / maxHealth;
        slider.value = healthRatio;
    }
}
