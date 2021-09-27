using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilloAnimEvents : MonoBehaviour
{
    [SerializeField] ArmadilloGun _armadilloGun;

    public void TailSwipeFire()
    {
        _armadilloGun.ShootBullet();
    }
}
