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

        // get number of players
        // number of player cards should equal number of players
        // playercards[] should link to players[]
        //


        //foreach (PlayerUICard card in playerCards)
        //{
        //    card.RemovePlayer();
        //}
        //// enable first UI player card
        //for (int i = 0; i < Gamemode.Instance.Players.Count; i++)
        //{
        //    playerCards[i].AddPlayer(Gamemode.Instance.Players[i]);
        //}
        Debug.Log(Gamemode.Instance.Players.Count);
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
