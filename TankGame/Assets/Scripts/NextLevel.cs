using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    GameObject NextLevelButton;

    public void Awake()
    {
        // Checks to see if there is a next level // TODO untested
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount - 1)
        {
            NextLevelButton.SetActive(false);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
