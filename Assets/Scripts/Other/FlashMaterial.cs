using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashMaterial : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] float _timeUntilFlash;
    [SerializeField] float _flashDuration;

    MeshList _meshList;

    private void Start()
    {
        _meshList = GetComponent<MeshList>();

        StartCoroutine(WaitAndInvisible(_timeUntilFlash));
    }

    private IEnumerator WaitAndInvisible(float time)
    {
        yield return new WaitForSeconds(time);
        _meshList.SetMaterial(_material);
        StartCoroutine(WaitAndVisible(_flashDuration));
    }

    private IEnumerator WaitAndVisible(float time)
    {
        yield return new WaitForSeconds(time);
        _meshList.RestoreMaterials();
        StartCoroutine(WaitAndInvisible(_flashDuration));
    }
}
