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
    [SerializeField, TextArea] private string trap1;
    [SerializeField, TextArea] private string trap2;
    [SerializeField, TextArea] private string hide;

    private int numTutorial;
    private bool completedTutorial;
    private bool feedBackBool;
    private bool paused;

    public bool Paused { get => paused; }

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (collision.CompareTag("DoorSistem"))
        {
            feedBackBool = true;
            numTutorial++;
            msg.text = hack;
        }
    }

    public void NotPause()
    {
        paused = false;
        feedBack.gameObject.SetActive(true);
    }
}