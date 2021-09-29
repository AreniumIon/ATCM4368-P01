using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryImage : MonoBehaviour
{
    [SerializeField] Health _bossHealth;
    [SerializeField] TextMeshProUGUI _victoryText;

    private void Start()
    {
        _victoryText.gameObject.SetActive(false);
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
        _victoryText.gameObject.SetActive(true);
    }
}
