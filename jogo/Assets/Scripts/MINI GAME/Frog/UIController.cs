using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private GameObject gameOver;

    private bool gameOverBool;
    private float timeCurrent;
    private Frog frog;

    public bool GameOverBool { get => gameOverBool; }

    // Start is called before the first frame update
    void Start()
    {
        frog = FindObjectOfType<Frog>();
        timeCurrent = 10;
    }

    // Update is called once per frame
    void Update()
    {
        TimeController();
    }

    private void TimeController()
    {
        if (!frog.Finish)
        {
            timeCurrent -= Time.deltaTime;
            bar.fillAmount = timeCurrent / 10;
        }

        if (timeCurrent <= 0 && !frog.Finish)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverBool = true;
        gameOver.SetActive(true);
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scene");
    }
}
