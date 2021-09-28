using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] Health _bossHealth;
    [SerializeField] Slider _slider;

    private void OnEnable()
    {
        _bossHealth.HealthChangedEvent += UpdateBossHealth;
    }

    private void OnDisable()
    {
        _bossHealth.HealthChangedEvent -= UpdateBossHealth;
    }

    public void UpdateBossHealth(int currentHealth, int maxHealth)
    {
        float healthRatio = 1f * currentHealth / maxHealth;
        _slider.value = healthRatio;
    }
}
