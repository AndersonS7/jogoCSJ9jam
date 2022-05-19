using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private GameObject success;

    private UIController ui;
    private bool pause;
    private bool finish;

    public bool Finish { get => finish; }
    
    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pause = true;
            ui.GameOver();
        }

        if (collision.CompareTag("Finish"))
        {
            PlayerPrefs.SetString("hack", "true");
            PlayerPrefs.Save();

            finish = true;
            pause = true;
            success.SetActive(true);
            StartCoroutine("Success");
        }
    }

    IEnumerator Success()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scene");
    }
}
