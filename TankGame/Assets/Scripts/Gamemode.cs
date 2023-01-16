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
    private List<AiTank> Enemies;

    [SerializeField]
    private GameObject[] Levels;
    [SerializeField]
    public int currentLevel { get; private set; } // TODO make this show as +1 in inspector to be less confusing than 0 based indexing

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
        playerSpawnLoc = GameObject.FindGameObjectsWithTag("spawn");
        RestartLevel();
    }

    private void LateUpdate()
    {
        if (!CheckEnemyAlive() && CheckPlayerAlive())
        {
            //Time.timeScale = 0.0f;
            LevelCompleteScreen.SetActive(true);
        }
        else if (!CheckPlayerAlive() && CheckEnemyAlive())
        {
            //Time.timeScale = 0.0f;
            GameOverScreen.SetActive(true);
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
        Players.Add(player);
        PlayerCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemovePlayer(PlayerTank player)
    { 
        Players.Remove(player);
        Destroy(player.gameObject);
        PlayerCountChanged(this, EventArgs.Empty);
    }

    public void NewEnemy(AiTank enemy)
    {
        Enemies.Add(enemy);
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

    public void LoadLevel(int level)
    {
        if (level < Levels.Length) 
        {
            // hide current level and activate the next level
            Destroy(GameObject.FindGameObjectWithTag("level"));
            currentLevel = level;
            Instantiate(Levels[level]);

            RespawnPlayers();
        }
        else
        {
            Debug.Log("Level outside of range");
        }
    }

    public void RestartLevel()
    {
        Destroy(GameObject.FindGameObjectWithTag("level"));
        Enemies.Clear();
        Instantiate(Levels[currentLevel]);
        playerSpawnLoc = GameObject.FindGameObjectsWithTag("spawn");
        RespawnPlayers();
        // TODO need to remove any bullets still in scene
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
