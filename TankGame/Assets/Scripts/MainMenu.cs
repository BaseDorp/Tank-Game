using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject MainMenuObject;
    [SerializeField]
    GameObject LevelSelectObject;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuObject.SetActive(true);
        LevelSelectObject.SetActive(false);
    }

    public void ClickedLevel(int levelNumber)
    {
        //Gamemode.Instance.LoadLevel(levelNumber);
        //Gamemode.Instance.currentLevel = levelNumber;
    }

    public void Sandbox()
    {
        //SceneManager.LoadScene(/*Scene Name*/);
    }    

    public void Settings()
    {
        // Settings
    }

    public void Quit()
    {
        Application.Quit();
    }
}
