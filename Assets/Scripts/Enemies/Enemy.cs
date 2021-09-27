using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MathFunctions;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;

    private Rigidbody _rb;
    protected Rigidbody Rb => _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        DoCollision(collision.gameObject);
    }

    // Separate so other scripts can simulate a collision (ArmadilloTail)
    public void DoCollision(GameObject collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        Debug.Log(collision.layer.ToString());
        if (damageable != null && IsMatchingLayer(LayerMask.GetMask("Player"), collision.layer))
        {
            Debug.Log("matching layers");
            bool success = PlayerImpact(collision, damageable);
            ImpactFeedback(success);
        }
    }

    // Return true if successful. Used for feedback
    protected virtual bool PlayerImpact(GameObject player, IDamageable playerDamageable)
    {
        return playerDamageable.TakeDamage(_damageAmount);
    }

    private void ImpactFeedback(bool success)
    {
        // Particles
        if (_impactParticles != null && success)
        {
            _impactParticles.Play();
        }
        // Audio
        if (_impactSound != null && success)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {

    }
}
