using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialWhenDamaged : MonoBehaviour
{
    [SerializeField] Health _health;
    [SerializeField] MeshList _meshList;
    [SerializeField] Material _damagedMaterial;
    [SerializeField] int _healthThreshold;

    private void OnEnable()
    {
        _health.TakeDamageEvent += CheckChangeMaterial;
    }

    private void OnDisable()
    {
        _health.TakeDamageEvent -= CheckChangeMaterial;
    }

    public void CheckChangeMaterial(int damageTaken, GameObject attacker)
    {
        if (_health.CurrentHealth - damageTaken <= _healthThreshold)
            ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        _meshList.SetMaterial(_damagedMaterial);
    }
}
