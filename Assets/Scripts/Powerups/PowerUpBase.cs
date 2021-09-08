using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);
    protected abstract void PowerDown(Player player);


    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed => _movementSpeed;

    [SerializeField] float _powerupDuration = 1;
    protected float PowerupDuration => _powerupDuration;


    [SerializeField] ParticleSystem _powerupParticles;
    [SerializeField] AudioClip _powerupSound;
    [SerializeField] AudioClip _powerdownSound;

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
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PowerUp(player);
            PowerUpFeedback();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(WaitAndPowerDown(PowerupDuration, player));
        }
    }

    protected IEnumerator WaitAndPowerDown(float time, Player player)
    {
        yield return new WaitForSeconds(time);
        PowerDown(player);
        PowerDownFeedback();
        gameObject.SetActive(false);
    }    

    private void PowerUpFeedback()
    {
        // Particles
        if (_powerupParticles != null)
        {
            _powerupParticles.Play();
        }
        // Audio
        if (_powerupSound != null)
        {
            AudioHelper.PlayClip2D(_powerupSound, 1f);
        }
    }

    private void PowerDownFeedback()
    {
        // Audio
        if (_powerdownSound != null)
        {
            AudioHelper.PlayClip2D(_powerdownSound, 1f);
        }
    }
}
