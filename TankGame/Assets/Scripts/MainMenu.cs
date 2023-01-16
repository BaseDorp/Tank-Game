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
        // -1 because I want the level 1 to show as 1 not 0 in the inspector // TODO fix so its the same across scripts
        Gamemode.Instance.LoadLevel(levelNumber-1);
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
