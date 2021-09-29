using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MathFunctions;

public class ArmadilloCurledCollision : MonoBehaviour
{
    [SerializeField] LayerMask _wallMask;
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
        CheckBounce(collision.gameObject);
    }

    public void CheckBounce(GameObject collision)
    {
        if (af.IsCurled && IsMatchingLayer(_wallMask, collision.layer))
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
