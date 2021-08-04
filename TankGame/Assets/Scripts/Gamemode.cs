using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    // Singleton Instance
    public static Gamemode Instance { get; private set; }

    // Moving tanks need to go to the closest last position of a tank
    // Radars should update the last known location of THAT tank (List of locations to show each last known loccation?)
    // only moving tanks need to know last known locations, and radar? or ai tanks should show too?
    // if tank sees player, update the last known location in the same spot as players list


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

    public void NewPlayer(PlayerTank player) // TODO make this not need an existing player instance?
    {
        Players.Add(player);
    }

    public void RemovePlayer(PlayerTank player)
    {
        Players.Remove(player);
    }
}
