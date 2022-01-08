using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerUICard : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerOptions;
    [SerializeField]
    GameObject NewPlayerButton;

    TMPro.TMP_Dropdown InputDropdown;

    PlayerTank playerTank;

    // Start is called before the first frame update
    void Start()
    {
        playerTank = Gamemode.Instance.Players[1]; // TODO not do this

        InputDropdown = GetComponentInChildren<TMP_Dropdown>();
        InputDropdown.ClearOptions();
        foreach (var device in InputSystem.devices)
        {
            // Only get inputs for keyboards, gamepads, and mobile touch
            InputDropdown.options.Add(new TMP_Dropdown.OptionData(device.displayName));
        }
    }

    public void ChangeInputDevice()
    {
        playerTank.playerInput.SwitchCurrentControlScheme(InputSystem.devices[InputDropdown.value]);
        //playerTank.ChangeInputDevice(InputDropdown.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(PlayerTank player = null) // TODO make not null
    {
        // check if player is not null?

        //this.playerTank = player;
        // assign the image preview to be the camera from playerTank

        PlayerOptions.SetActive(true);
        NewPlayerButton.SetActive(false);
    }

    public void RemovePlayer()
    {
        // Makes sure you can't remove player 1
        if (this.playerTank != Gamemode.Instance.Players[0])
        {
            this.playerTank = null;
            PlayerOptions.SetActive(false);
            NewPlayerButton.SetActive(true);
        }
    }
}
