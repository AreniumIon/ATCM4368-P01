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


    public void ShootBullet()
    {
        Instantiate(_bulletPrefab, _swipeSpawn.position, Quaternion.Euler(0f, _swipeSpawn.rotation.eulerAngles.y, 0f));
    }

    public void Stomp()
    {
        for (int i  = 0; i < _stompBoxes; i++)
        {
            Instantiate(_boxPrefab, _jumpSpawn.position, Quaternion.Euler(0f, _jumpSpawn.rotation.eulerAngles.y, 0f));
        }
    }
}
