using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumMan;

public class ArmadilloGun : MonoBehaviour
{
    [Header("Spawn Positions")]
    [SerializeField] Transform _swipeSpawn;
    [SerializeField] Transform _jumpSpawn;

    [Header("Prefabs")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _boxPrefab;

    [Header("Values")]
    [SerializeField] int _stompBoxes;
    [SerializeField] float _boxOffset;
    [SerializeField] float _boxForce;
    [SerializeField] float _boxForceVariance;
    [SerializeField] float _boxAngleVariance;

    [Header("Feedback")]
    [SerializeField] ParticleSystem _swipeParticles;
    [SerializeField] AudioClip _swipeSound;
    [SerializeField] float _swipeSoundVolume;
    [SerializeField] ParticleSystem _stompParticles;
    [SerializeField] AudioClip _stompSound;
    [SerializeField] float _stompSoundVolume;

    static float _bulletHeight = .5f;

    public void Swipe()
    {
        SwipeFeedback();
    }

    private void SwipeFeedback()
    {
        // Particles
        if (_swipeParticles != null)
        {
            _swipeParticles.Play();
        }
        // Audio
        if (_swipeSound != null)
        {
            AudioHelper.PlayClip2D(_swipeSound, _swipeSoundVolume);
        }
    }

    public void ShootBullet()
    {
        Vector3 position = new Vector3(_swipeSpawn.position.x, _bulletHeight, _swipeSpawn.position.z);
        Instantiate(_bulletPrefab, _swipeSpawn.position, Quaternion.Euler(0f, _swipeSpawn.rotation.eulerAngles.y, 0f));
    }

    public void Stomp()
    {
        for (int i  = 0; i < _stompBoxes; i++)
        {
            SpawnBox(i * 360 / _stompBoxes);
        }

        StompFeedback();
    }

    private void StompFeedback()
    {
        // Particles
        if (_stompParticles != null)
        {
            _stompParticles.Play();
        }
        // Audio
        if (_stompSound != null)
        {
            AudioHelper.PlayClip2D(_stompSound, _stompSoundVolume);
        }
    }

    private void SpawnBox(float angle)
    {
        // Randomize angle
        angle += Random.Range(-1 * _boxAngleVariance, _boxAngleVariance);

        // Spawn box
        Vector3 offset = Vector3.forward * _boxOffset;
        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
        GameObject newBox = Instantiate(_boxPrefab, _jumpSpawn.position + rotation * offset, rotation);

        // Apply force
        Rigidbody rb = newBox.GetComponent<Rigidbody>();
        float force = _boxForce;
        force += Random.Range(-1 * _boxForceVariance, _boxForceVariance);
        rb.AddForce(rotation * new Vector3(0f, 0f, force));
    }


}
