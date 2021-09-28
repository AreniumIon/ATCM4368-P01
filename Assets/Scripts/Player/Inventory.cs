using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] int _startingTreasure = 0;
    int treasure;
    public int Treasure
    {
        get => treasure;
        set
        {
            treasure = value;
        }
    }

    private void Awake()
    {
        Treasure = _startingTreasure;
    }

}
