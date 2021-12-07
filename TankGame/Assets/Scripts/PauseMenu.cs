using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuUI;

    public Slider[] colorSliders;
    public Image colorHandle;

    // Start is called before the first frame update
    void Start()
    {
        MenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // checks if menu is active (already displayed)
            if (MenuUI.activeInHierarchy)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ValueChangeCheck()
    {
        foreach (Slider s in colorSliders)
        {
            colorHandle.color = Color.HSVToRGB(s.value, 1, 1);
        }
    }

    public void Pause()
    {
        MenuUI.SetActive(true);
        Time.timeScale = 0f;

        foreach (Slider s in colorSliders)
        {
            s.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }
    }

    public void Resume()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Settings()
    {
        //Time.timeScale = 1f;
        // TODO load settings
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
