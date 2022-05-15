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
    private bool inPoint;

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
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.4f, 0);
        Collider2D hit = Physics2D.OverlapCircle(pos, radius, layerElevator);

        //controla a entrada no elevador
        if (hit != null)
        {
            if (Input.GetKey(KeyCode.E) && !isElevator)
            {
                inPoint = true;
                isElevator = true;
                tagElevator = hit.gameObject.tag;

                if (inPoint)
                {
                    transform.position = hit.transform.position;
                    inPoint = false;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.4f, 0);
        Gizmos.DrawWireSphere(pos, radius);
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
