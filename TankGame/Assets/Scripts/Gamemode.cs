﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    // Singleton Instance
    public static Gamemode Instance { get; private set; }

    [SerializeField]
    public List<PlayerTank> Players;
    [SerializeField]
    GameObject PlayerPrefab;

    // Location where new players will spawn in
    [SerializeField]
    public Transform PlayerStartLocation;

    void Awake()
    {
        // Creates instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Limits fps count
        Application.targetFrameRate = 60;

    }

    private void Start()
    {
        // Player 1
        //Instantiate(PlayerPrefab);
    }

    public void NewPlayer(PlayerTank player) // TODO make this not need an existing player instance?
    {
        player.transform.position = PlayerStartLocation.position;
        Players.Add(player);
    }

    public void RemovePlayer(PlayerTank player)
    { 
        Players.Remove(player);
        //Destroy(player.gameObject);
    }
}
