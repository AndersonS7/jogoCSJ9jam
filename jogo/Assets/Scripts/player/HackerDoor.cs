using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerDoor : MonoBehaviour
{
    private float timeCount;
    private int number;
    private bool isHackDoor;

    void Start()
    {

    }

    void Update()
    {
        HackingDoor();
        SuccesHack();
    }

    //esse método é responssável por levantar a porta
    private void SuccesHack()
    {
        if (number >= 40)
        {
            timeCount += Time.deltaTime;

            if (timeCount <= 2.5f)
            {
                transform.Translate(Vector2.up * 1f * Time.deltaTime);
            }
        }
    }

    //esse método inicia o hack
    private void HackingDoor()
    {
        if (isHackDoor && Input.GetKeyDown(KeyCode.E))
        {
            number = Random.Range(1, 100);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isHackDoor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isHackDoor = false;
        }
    }
}
