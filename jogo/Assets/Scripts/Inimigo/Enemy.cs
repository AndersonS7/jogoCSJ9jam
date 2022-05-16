using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask layerWall;
    [SerializeField] private LayerMask layerPlayer;

    private Vector2 direction;
    private int directionAux;

    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        direction = new Vector2(1, 0);
        directionAux = 1;
    }

    void FixedUpdate()
    {
        ToMove();
    }

    private void ToMove()
    {
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //quando bater na parede
        if (collision.CompareTag("Wall"))
        {
            if (directionAux == 1)
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = new Vector2(-1, 0);
                directionAux = -1;
            }
            else if (directionAux == -1)
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = new Vector2(1, 0);
                directionAux = 1;
            }
        }
    }
}
