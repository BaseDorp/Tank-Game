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
    [SerializeField]
    public List<AiTank> Enemies;

    // Location where new players will spawn in
    [SerializeField]
    public Transform PlayerStartLocation;

    [SerializeField]
    GameObject GameOverScreen;
    [SerializeField]
    GameObject LevelCompleteScreen;

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
        // TODO can make this array if the enemies arent getting changed at run time
        Enemies = new List<AiTank>(FindObjectsOfType<AiTank>());
    }

    private void Update()
    {
        if (!CheckEnemyAlive())
        {
            //Time.timeScale = 0.0f;
            LevelCompleteScreen.SetActive(true);
            Debug.Log("Level Clear. All enemies defeated");
        }
        else if (!CheckPlayerAlive())
        {
            //Time.timeScale = 0.0f;
            GameOverScreen.SetActive(true);
            Debug.Log("Game Over. All players dead");
        }

    }

    public bool CheckPlayerAlive()
    {
        if (Players.Count < 1)
        {
            return false;
        }

        foreach (PlayerTank p in Players)
        {
            if (p.bTankAlive)
            {
                return true; // returns if any of the players are still alive
            }

        }
        return false;
    }

    public bool CheckEnemyAlive()
    {
        if (Enemies.Count < 1)
        {
            return false;
        }

        foreach (AiTank e in Enemies)
        {
            if (e.bTankAlive)
            {
                return true; // returns if any of the enemies are still alive
            }
        }

        return false;
    }

    public void NewPlayer(PlayerTank player)
    {
        //player.transform.position = PlayerStartLocation.position; // TODO i dont think it was moving it to the right location so not doing this rn
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
