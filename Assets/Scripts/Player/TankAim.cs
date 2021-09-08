using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAim : MonoBehaviour
{
    [SerializeField] Transform _turretTransform;

    private void Update()
    {
        AimTurret();
    }

    private void AimTurret()
    {
        // Set up plane
        Plane plane = new Plane(Vector3.up, 0f);
        
        // Get mouse position
        Vector3 mousePos = Input.mousePosition;

        // Raycast
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        float distance;
        Vector3 worldPos;

        if (plane.Raycast(ray, out distance))
        {
            worldPos = ray.GetPoint(distance);

            // Aim at mouse
            _turretTransform.LookAt(worldPos);
        }

        // Adjust y-position
        //Vector3 targetPos = new Vector3(mouseWorldPos.x, _turretTransform.position.y, mouseWorldPos.z);


    }
}
