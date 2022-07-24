using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Gamemode : MonoBehaviour
{
    // Singleton Instance
    public static Gamemode Instance { get; private set; }

    public event EventHandler PlayerCountChanged;
    [SerializeField]
    public List<PlayerTank> Players;
    [SerializeField]
    private AiTank[] Enemies;

    [SerializeField]
    private GameObject[] Levels;
    [SerializeField]
    int currentLevel; // TODO make this show as +1 in inspector to be less confusing than 0 based indexing

    // Location where new players will spawn in
    [SerializeField]
    public GameObject[] playerSpawnLoc;
    

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
        }
        else
        {
            Destroy(gameObject);
        }

        // Limits fps count
        Application.targetFrameRate = 60;
        //Instantiate(Levels[currentLevel]);
    }

    private void Start()
    {
        Enemies = FindObjectsOfType<AiTank>();

        playerSpawnLoc = GameObject.FindGameObjectsWithTag("spawn");
        
    }

    private void Update()
    {
        if (!CheckEnemyAlive() && CheckPlayerAlive())
        {
            //Time.timeScale = 0.0f;
            LevelCompleteScreen.SetActive(true);
            Debug.Log("Level Clear. All enemies defeated");
        }
        else if (!CheckPlayerAlive() && CheckEnemyAlive())
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
        if (Enemies.Length < 1)
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

    public void NextLevel()
    {
        if (currentLevel >= Levels.Length)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
            return;
        }

        // hide current level and activate the next level
        Destroy(GameObject.FindGameObjectWithTag("level"));
        currentLevel++;
        Instantiate(Levels[currentLevel]);
        RespawnPlayers();
    }

    public void RestartLevel()
    {
        Destroy(GameObject.FindGameObjectWithTag("level"));
        Instantiate(Levels[currentLevel]);
        RespawnPlayers();
    }

    void RespawnPlayers()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].transform.position = playerSpawnLoc[i].transform.position;
            Players[i].enabled = true;
        }
    }
}
