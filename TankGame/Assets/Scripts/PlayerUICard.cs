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

    TMPro.TMP_Dropdown InputDropdown;
    Slider ColorSlider;

    public PlayerTank playerTank; // TODO

    // Start is called before the first frame update
    void Start()
    {
        playerTank = Gamemode.Instance.Players[0]; // TODO not do this

        InputDropdown = GetComponentInChildren<TMP_Dropdown>();
        ColorSlider = GetComponentInChildren<Slider>();

        InputDropdown.ClearOptions();
        foreach (var device in InputSystem.devices)
        {
            // Only get inputs for keyboards, gamepads, and mobile touch
            InputDropdown.options.Add(new TMP_Dropdown.OptionData(device.displayName));
        }
        // TODO there is probably a better way to get the input device that the player is using without having to make the player input component public
        InputDropdown.captionText.SetText(playerTank.playerInput.devices[0].displayName);
        ChangeInputDevice();
        // Also when changing the dropdown it still says that keyboard is selected
    }

    public void ChangeInputDevice()
    {
        playerTank.ChangeInputDevice(InputDropdown.value);
    }

    public void ColorChanged()
    {
        Color newColor = Color.HSVToRGB(ColorSlider.value, 0.8f, 0.8f);
        if (playerTank != null)
        {
            playerTank.SetColor(new Color(newColor.r, newColor.g, newColor.b));
        }
    }

    public void RemovePlayer()
    {
        // Makes sure you can't remove if there is only 1 player
        if (Gamemode.Instance.Players.Count != 1)
        {
            PlayerOptions.SetActive(false);
            Gamemode.Instance.RemovePlayer(playerTank);
            this.playerTank = null;
        }
    }
}
