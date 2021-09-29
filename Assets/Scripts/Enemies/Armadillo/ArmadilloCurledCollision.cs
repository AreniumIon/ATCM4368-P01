using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MathFunctions;

public class ArmadilloCurledCollision : MonoBehaviour
{
    [SerializeField] LayerMask _groundMask;
    [SerializeField] ParticleSystem _bounceParticles;
    [SerializeField] AudioClip _bounceSound;
    [SerializeField] float _bounceSoundVolume;

    ArmadilloForms af;

    private void Start()
    {
        af = GetComponent<ArmadilloForms>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (af.IsCurled && IsMatchingLayer(_groundMask, collision.gameObject.layer))
            BounceFeedback();
    }

    private void BounceFeedback()
    {
        // Particles
        if (_bounceParticles != null)
        {
            _bounceParticles.Play();
        }
        // Audio
        if (_bounceSound != null)
        {
            AudioHelper.PlayClip2D(_bounceSound, _bounceSoundVolume);
        }
    }

}
