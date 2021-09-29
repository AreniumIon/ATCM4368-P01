using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTextOnDeath : MonoBehaviour
{
    [SerializeField] Health _bossHealth;
    [SerializeField] TextMeshProUGUI _text;

    private void Start()
    {
        _text.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _bossHealth.DeathEvent += ShowVictoryText;
    }

    private void OnDisable()
    {
        _bossHealth.DeathEvent -= ShowVictoryText;
    }

    public void ShowVictoryText()
    {
        _text.gameObject.SetActive(true);
    }
}
