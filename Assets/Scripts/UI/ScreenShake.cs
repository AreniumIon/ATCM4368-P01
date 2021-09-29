using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] Health _playerHealth;
    [SerializeField] float _duration;
    [SerializeField] float _magnitude;


    Vector3 _originalPos;

    private void Start()
    {
        _originalPos = transform.position;
    }

    private void OnEnable()
    {
        _playerHealth.TakeDamageEvent += ShakeScreen;
    }

    private void OnDisable()
    {
        _playerHealth.TakeDamageEvent -= ShakeScreen;
    }

    public void ShakeScreen(int damageTaken, GameObject attacker)
    {
        StartCoroutine(Shake(_duration, _magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, 0f, z) + _originalPos;

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = _originalPos;
    }
}
