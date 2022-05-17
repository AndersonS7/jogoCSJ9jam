using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGoo : MonoBehaviour
{
    private Transform player;
    private Vector3 playerPos;
    private Rigidbody2D rig;
    private bool isAddForce; //faz com que seja adicionado o impulso só uma vez

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos = new Vector2(player.position.x, player.position.y + 1.5f);
    }

    private void FixedUpdate()
    {
        if (!isAddForce)
        {
            isAddForce = true;
            rig.AddForce(Vector2.up * 250 * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position == playerPos)
        {
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, playerPos, 10 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject, 0.1f);
        }
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
