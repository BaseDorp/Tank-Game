using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuUI;

    public Slider[] colorSliders;
    public Image[] colorHandle;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        colorSliders = GetComponents<Slider>();
        MenuUI.SetActive(true);

        Debug.Log(InputSystem.devices.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChangeCheck()
    {
        for (int i = 0; i < Gamemode.Instance.Players.Count; i++)
        {
            colorHandle[i].color = Color.HSVToRGB(colorSliders[i].value, 1, 1);
        }
//         foreach (Slider s in colorSliders)
//         {
//             colorHandle.color = Color.HSVToRGB(s.value, 1, 1);
//         }
    }

    public void Pause()
    {
        if (isPaused)
        {
            MenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            MenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        foreach (Slider s in colorSliders)
        {
            s.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }
    }

    public void Resume()
    {
        
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void NewPlayer()
    {

    }
}
