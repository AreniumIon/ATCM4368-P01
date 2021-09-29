using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;
    [SerializeField] Slider _yellowSlider;
    [SerializeField] float _depleteSpeed;

    public void SetValue(float value)
    {
        float valueChange = _healthSlider.value - value;

        _healthSlider.value = value;

        if (valueChange > 0)
            StartCoroutine(DepleteYellow(valueChange));
    }

    private IEnumerator DepleteYellow(float amount)
    {
        float elapsed = 0f;

        while (elapsed < amount / _depleteSpeed)
        {
            _yellowSlider.value = _yellowSlider.value - _depleteSpeed * Time.deltaTime;

            elapsed += Time.deltaTime;

            yield return null;
        }

    }
}
