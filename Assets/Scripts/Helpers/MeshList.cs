using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshList : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> _meshRenderers = new List<MeshRenderer>();

    private List<Material> _materials = new List<Material>();

    private void Start()
    {
        foreach (MeshRenderer mr in _meshRenderers)
        {
            _materials.Add(mr.material);
        }
    }

    public void SetMaterial(Material m)
    {
        foreach (MeshRenderer mr in _meshRenderers)
        {
            mr.material = m;
        }
    }

    public void DelayRestoreMaterials(float time)
    {
        StartCoroutine(WaitAndRestoreMaterials(time));
    }

    private IEnumerator WaitAndRestoreMaterials(float time)
    {
        yield return new WaitForSeconds(time);
        RestoreMaterials();
    }

    public void RestoreMaterials()
    {
        int i = 0;
        foreach (MeshRenderer mr in _meshRenderers)
        {
            mr.material = _materials[i++];
        }
    }
}
