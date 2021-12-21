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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedLevel(int levelNumber)
    {
        SceneManager.LoadScene(sceneBuildIndex: levelNumber);
    }

    public void Sandbox()
    {
        //SceneManager.LoadScene(/*Scene Name*/);
    }    

    public void Quit()
    {
        Application.Quit();
    }
}
