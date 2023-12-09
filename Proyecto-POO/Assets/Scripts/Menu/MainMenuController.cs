using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void StartLevel(){
        SceneManager.LoadScene(1);
    }
    public void QuitGame(){
        Application.Quit();
    }
}


