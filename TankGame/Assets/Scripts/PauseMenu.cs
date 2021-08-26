using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuUI;

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

    public void Pause()
    {
        MenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Settings()
    {
        Time.timeScale = 1f;
        // TODO load settings
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
