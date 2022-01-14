using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gamemode : MonoBehaviour
{
    // Singleton Instance
    public static Gamemode Instance { get; private set; }

    public event EventHandler PlayerCountChanged;
    [SerializeField]
    public List<PlayerTank> Players;
    //[SerializeField]
    //GameObject PlayerPrefab;

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
        
    }

    public void NewPlayer(PlayerTank player)
    {
        //player.transform.position = PlayerStartLocation.position;
        Players.Add(player);
        PlayerCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemovePlayer(PlayerTank player)
    { 
        Players.Remove(player);
        Destroy(player.gameObject);
        PlayerCountChanged(this, EventArgs.Empty);
    }
}
