using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerUICard : MonoBehaviour
{
    TMPro.TMP_Dropdown InputDropdown;

    // Start is called before the first frame update
    void Start()
    {
//         Component[] allComponents = GetComponentsInChildren<Component>();
//         foreach (var c in allComponents)
//         {
//             Debug.Log(c.ToString());
//         }
        InputDropdown = GetComponentInChildren<TMP_Dropdown>();
        InputDropdown.options.Add(new TMP_Dropdown.OptionData(InputSystem.devices.ToString()));
        Debug.Log(InputSystem.devices.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
