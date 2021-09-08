using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected virtual void Collide(GameObject collision)
    {
        CollideFeedback();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Lifetime = _collideParticles.main.duration;
    }

    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed => _movementSpeed;

    [SerializeField] float _lifetime = 1;
    protected float Lifetime
    { 
        get { return _lifetime; }
        set 
        {
            _lifetime = Mathf.Max(value, 0f);
            if (_lifetime <= 0)
                Die();
        }
    }


    [SerializeField] ParticleSystem _collideParticles;
    [SerializeField] AudioClip _collideSound;

    Rigidbody _rb;

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        Lifetime -= Time.fixedDeltaTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        Collide(other.gameObject);
    }

    protected void CollideFeedback()
    {
        // Particles
        if (_collideParticles != null)
        {
            _collideParticles.Play();
        }
        // Audio
        if (_collideSound != null)
        {
            AudioHelper.PlayClip2D(_collideSound, 1f);
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
