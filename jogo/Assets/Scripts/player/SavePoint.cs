using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Transform target;

    private int save;
    private int playerDead;

    void Start()
    {
        save = PlayerPrefs.GetInt("save");
        playerDead = PlayerPrefs.GetInt("dead");

        if (save == 1 && playerDead == 1)
        {
            CheckPoint();

            PlayerPrefs.DeleteKey("dead");
        }
    }

    public void CheckPoint()
    {
        target.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("save", 1);
            Debug.Log("Checkpoint...");
        }
    }
}
