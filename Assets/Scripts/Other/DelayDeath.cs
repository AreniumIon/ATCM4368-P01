using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDeath : MonoBehaviour
{
    [SerializeField] float _lifetime;

    private void Start()
    {
        StartCoroutine(WaitAndDie(_lifetime));
    }

    private IEnumerator WaitAndDie(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
