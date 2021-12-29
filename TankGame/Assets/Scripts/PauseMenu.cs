using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuUI;
    [SerializeField]
    PlayerUICard[] playerCards;

    public Slider[] colorSliders;
    public Image[] colorHandle;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        colorSliders = GetComponents<Slider>();
        this.Pause();

        foreach (PlayerUICard card in playerCards)
        {
            card.RemovePlayer();
        }
        // enable first UI player card and call UI player card to disable the others
        for (int i = 0; i < Gamemode.Instance.Players.Count; i++)
        {
            playerCards[i].AddPlayer(Gamemode.Instance.Players[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        if (isPaused)
        {
            MenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
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
