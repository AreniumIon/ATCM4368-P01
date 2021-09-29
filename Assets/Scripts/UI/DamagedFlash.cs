using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagedFlash : MonoBehaviour
{
    [SerializeField] Health _playerHealth;
    [SerializeField] float _duration;
    [SerializeField] float _opacity;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();

        SetOpacity(0f);
    }

    private void OnEnable()
    {
        _playerHealth.TakeDamageEvent += TakeDamageFlash;
    }

    private void OnDisable()
    {
        _playerHealth.TakeDamageEvent -= TakeDamageFlash;
    }

    public void TakeDamageFlash(int damageTaken, GameObject attacker)
    {
        StartCoroutine(DoFlash(_duration, _opacity));
    }

    private IEnumerator DoFlash(float duration, float opacity)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float currentOpacity = CalculateOpacity(elapsed / duration, opacity);

            SetOpacity(currentOpacity);

            elapsed += Time.deltaTime;

            yield return null;
        }
    }
    
    private float CalculateOpacity(float timeRatio, float maxOpacity)
    {
        return 2 * (.5f - Mathf.Abs(timeRatio - .5f)) * maxOpacity;
    }

    private void SetOpacity(float opacity)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
    }
}
