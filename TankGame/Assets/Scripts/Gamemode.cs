using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    [SerializeField]
    int NumberOfPlayers;
    [SerializeField]
    PlayerTank[] Players;

    void Awake()
    {
        // Limits fps count
        //Application.targetFrameRate = 60;

    }
}
