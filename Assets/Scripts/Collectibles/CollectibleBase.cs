using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CollectibleBase : MonoBehaviour
{
    protected abstract void Collect(Health player);

    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed => _movementSpeed;


    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        // Rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health player = other.gameObject.GetComponent<Health>();
        if (player != null && other.gameObject.GetComponent<TankController>() != null)
        {
            Collect(player);
            Feedback();
            gameObject.SetActive(false);
        }
    }

    private void Feedback()
    {
        // Particles
        if (_collectParticles != null)
        {
            // Instantiate particle system b/c collectible will be destroyed
            _collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
            _collectParticles.transform.localScale = new Vector3(1, 1, 1);
            _collectParticles.Play();
        }
        // Audio
        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }
}
