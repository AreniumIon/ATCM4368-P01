using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFeedback : MonoBehaviour
{
    [SerializeField] Health health;

    [SerializeField] Material _damagedMaterial;
    [SerializeField] float _damagedFlashTime;
    [SerializeField] AudioClip _damagedSound;
    [SerializeField] float _damagedSoundVolume;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] AudioClip _deathSound;
    [SerializeField] float _deathSoundVolume;

    private void OnEnable()
    {
        health.TakeDamageEvent += TakeDamageFeedback;
        health.DeathEvent += DeathFeedback;
    }

    private void OnDisable()
    {
        health.TakeDamageEvent -= TakeDamageFeedback;
        health.DeathEvent -= DeathFeedback;
    }

    protected virtual void TakeDamageFeedback(int damageAmount, GameObject attacker)
    {
        // Visuals
        if (_damagedMaterial != null)
        {
            MeshList meshList = gameObject.GetComponent<MeshList>();
            meshList?.SetMaterial(_damagedMaterial);
            meshList?.DelayRestoreMaterials(_damagedFlashTime);
        }

        // Audio
        if (_damagedSound != null)
        {
            AudioHelper.PlayClip2D(_damagedSound, _damagedSoundVolume);
        }
    }

    public void DeathFeedback()
    {
        // Particles
        if (_deathParticles != null)
        {
            ParticleSystem ps = Instantiate(_deathParticles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration);
        }
        // Audio
        if (_deathSound != null)
        {
            AudioHelper.PlayClip2D(_deathSound, _deathSoundVolume);
        }
    }
}
