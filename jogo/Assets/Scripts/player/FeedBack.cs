using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedBack : MonoBehaviour
{
    [SerializeField] private Image feedBack;
    [SerializeField] private Button next;
    [SerializeField] private Text msg;
    [SerializeField, TextArea] private string hack;
    [SerializeField, TextArea] private string elevator;
    [SerializeField, TextArea] private string trap;
    [SerializeField, TextArea] private string hide;

    private int tutorialNum;
    private bool feedBackBool;
    private bool paused;

    public bool Paused { get => paused; }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("tutor") > -1)
        {
            tutorialNum = PlayerPrefs.GetInt("tutor");
        }
        else
        {
            tutorialNum = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (feedBackBool)
        {
            paused = true;
            feedBack.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DoorSistem") && tutorialNum < 0)
        {
            feedBackBool = true;
            tutorialNum = 0;
            msg.text = hack;
            PlayerPrefs.SetInt("tutor", tutorialNum);
        }
        if (collision.CompareTag("Box") && tutorialNum < 1)
        {
            feedBackBool = true;
            tutorialNum = 1;
            msg.text = hide;
            PlayerPrefs.SetInt("tutor", tutorialNum);
        }
        if (collision.CompareTag("Elevator") && tutorialNum < 2)
        {
            feedBackBool = true;
            tutorialNum = 2;
            msg.text = elevator;
            PlayerPrefs.SetInt("tutor", tutorialNum);
        }
        if (collision.CompareTag("Finish") && tutorialNum < 3) //armadilha
        {
            feedBackBool = true;
            tutorialNum = 3;
            msg.text = trap;

            PlayerPrefs.SetInt("tutor", tutorialNum);
        }
    }

    public void NotPause()
    {
        feedBackBool = false;
        paused = false;
        feedBack.gameObject.SetActive(false);
    }
}
