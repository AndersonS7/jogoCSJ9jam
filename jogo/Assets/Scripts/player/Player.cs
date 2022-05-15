using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerElevator;

    private string tagElevator;
    private Rigidbody2D rig;
    private Vector2 direction;
    private bool isElevator;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DetectCollider();
        MoveElevator();
    }

    void FixedUpdate()
    {
        if (!isElevator)
        {
            ToMove();
        }
    }

    void ToMove()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);
    }

    void MoveElevator()
    {
        if (tagElevator == "PontoA" && isElevator)
        {
            transform.Translate(Vector3.down * 1f * Time.deltaTime);
        }

        if (tagElevator == "PontoB" && isElevator)
        {
            transform.Translate(Vector3.up * 1f * Time.deltaTime);
        }
    }

    void DetectCollider()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layerElevator);

        //controla a entrada no elevador
        if (hit != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                isElevator = true;
                tagElevator = hit.gameObject.tag;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    //target
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PontoB"))
        {
            isElevator = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Elevator"))
        {
            isElevator = false;
        }
    }
}
