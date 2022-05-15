using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HackerDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject progressBar;
    [SerializeField] Image barImg;

    private float timeCount;
    private int number;
    private bool isHackDoor;

    void Start()
    {
        progressBar.SetActive(false);
        barImg.fillAmount = 0;
        timeCount = 0;
        number = 0;
        isHackDoor = false;
    }

    void Update()
    {
        HackingDoor();
        SuccesHack();
    }

    //esse método é responssável por levantar a porta
    private void SuccesHack()
    {
        if (number >= 95)
        {
            timeCount += Time.deltaTime;

            if (timeCount <= 2.5f)
            {
                door.transform.Translate(Vector2.up * 1f * Time.deltaTime);
            }
        }
    }

    //esse método inicia o hack
    private void HackingDoor()
    {
        if (isHackDoor && Input.GetKeyDown(KeyCode.E))
        {
            number = Random.Range(1, 100);

            if (number >= 95)
            {
                Debug.Log("Success " + number);
            }
            else
            {
                Debug.Log("Failed " + number);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isHackDoor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isHackDoor = false;
        }
    }
}
