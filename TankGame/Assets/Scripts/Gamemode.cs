using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class Gamemode : MonoBehaviour
{
    // Singleton Instance
    public static Gamemode Instance { get; private set; }
    [SerializeField]
    PauseMenu pauseMenu;

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
    }

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void LateUpdate()
    {

        // TODO these checks would be more effecient as coroutines that is called whenever a tank dies
        if (Enemies.Count == 0)
        {
            return;
        }
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
            return false; // TODO see comment in CheckEnemyAlive()
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
            return true; // TODO technically this should return false but the PauseMenu script calls this so setting it to true allows the game to pause before the enemies are spawned
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

    // Used by the Level Complete UI
    public void NextLevel()
    {
        if (currentLevel >= Levels.Length)
        {
            Time.timeScale = 1f;
            // Return to Main Menu
            return;
        }

        // hide current level and activate the next level
        Destroy(GameObject.FindGameObjectWithTag("level"));
        currentLevel++;
        Instantiate(Levels[currentLevel]);
        //playerSpawnLoc = GameObject.FindGameObjectsWithTag("spawn");
        RespawnPlayers();
    }

    // Used by the Main Menu level select
    public void LoadLevel(int level)
    {
        level--;
        if (level < Levels.Length) 
        {
            // hide current level and activate the next level
            Destroy(GameObject.FindGameObjectWithTag("level"));
            currentLevel = level;
            Instantiate(Levels[level]);
            //playerSpawnLoc = GameObject.FindGameObjectsWithTag("spawn");
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
            Players[i].enabled = true;
            //Players[i].transform.position = playerSpawnLoc[i].transform.position; 
            Debug.Log(playerSpawnLoc.Length);
        }

        // Pause game // bring up lobby menu
        pauseMenu.Pause();
    }
}
