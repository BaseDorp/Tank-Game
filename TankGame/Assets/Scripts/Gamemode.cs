using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    // Singleton Instance
    public static Gamemode Instance { get; private set; }

    [SerializeField]
    int NumberOfPlayers;
    [SerializeField]
    public List<PlayerTank> Players;

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

    public void NewPlayer()
    {
        Players.Add(new PlayerTank());
    }

    public void RemovePlayer(PlayerTank player)
    {
        Players.Remove(player);
    }
}
