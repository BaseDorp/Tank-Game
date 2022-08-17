using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuUI;
    [SerializeField]
    PlayerUICard[] UICards;
    [SerializeField]
    GameObject AddInputMessage; // text box that says more player can join

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        this.Pause();
        Gamemode.Instance.PlayerCountChanged += UpdateUICards;

        // TODO copied from UpdateUICards. Find a way to call the method instead of copying
        for (int i = 1; i < UICards.Length; i++)
        {
            UICards[i].gameObject.SetActive(false);
        }
        // Sets the number of cards equal to how many players are in the game
        for (int i = 0; i < Gamemode.Instance.Players.Count; i++)
        {
            UICards[i].gameObject.SetActive(true);
            UICards[i].playerTank = Gamemode.Instance.Players[i];
        }

        if (Gamemode.Instance.Players.Count >= 4)
        {
            AddInputMessage.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Gamemode.Instance.PlayerCountChanged -= UpdateUICards;
    }

    public void UpdateUICards(object sender, EventArgs e)
    {
        for (int i = 1; i < UICards.Length; i++)
        {
            UICards[i].gameObject.SetActive(false);
        }
        // Sets the number of cards equal to how many players are in the game
        for (int i = 0; i < Gamemode.Instance.Players.Count; i++)
        {
            UICards[i].gameObject.SetActive(true);
            UICards[i].playerTank = Gamemode.Instance.Players[i];
        }

        if (Gamemode.Instance.Players.Count >= 4)
        {
            AddInputMessage.SetActive(false);
        }
    }

    public void Pause()
    {
        if (isPaused)
        {
            MenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else if (Gamemode.Instance.CheckPlayerAlive() && Gamemode.Instance.CheckEnemyAlive())
        {
            MenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
