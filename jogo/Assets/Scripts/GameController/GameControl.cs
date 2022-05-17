using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    [SerializeField] GameObject gameOver;

    private void Start()
    {
        Time.timeScale = 1;
        gameOver.SetActive(false);
    }

    //menu control
    public void ReloadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
