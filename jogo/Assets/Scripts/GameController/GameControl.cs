using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject sair;

    private bool pause;

    public bool Pause { get => pause; }

    private void Start()
    {
        Time.timeScale = 1;
        gameOver.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = true;
            sair.SetActive(true);
        }
    }
    
    //votlar ao jogo
    public void ReturnGame()
    {
        pause = false;
        sair.SetActive(false);
    }

    //menu control
    public void ReloadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
