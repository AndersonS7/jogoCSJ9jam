using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerElevator;
    [SerializeField] GameObject gameOver;
    [SerializeField] private int amountNumberScape ;//armazena os clicks

    private float saveSpeed;
    private string tagElevator;
    private Rigidbody2D rig;
    private Vector2 direction;

    private bool isElevator;
    private bool inPoint; // seta a posição quando está no elevador
    private bool inPointHacking; // seta a posição quando está hackeando

    //controla quando o inimigo atinge o player
    private bool isStuck; //indica quando o player está preso;
    private int numberScape; //numero de vezes que o player deve clicar para escapar

    private HackerDoor systemDoor;

    public bool IsElevator { get => isElevator; }
    public HackerDoor SystemDoor { get => systemDoor; }
    public Vector2 Direction { get => direction; }
    public bool IsStuck { get => isStuck; }

    void Start()
    {
        saveSpeed = speed;
        systemDoor = FindObjectOfType<HackerDoor>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetPosition();
        DetectCollider();
        MoveElevator();
        Scape();
    }

    void FixedUpdate()
    {
        if (!isElevator && !systemDoor.IsHaking)
        {
            ToMove();
        }
    }

    void ToMove()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);

        if (direction.x > 0)
        {
            transform.localScale = new Vector2(2, 2);
        }
        if (direction.x < 0)
        {
            transform.localScale = new Vector2(-2, 2);
        }
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

    void Scape()//quando o jogador estiver preso faz com que ele se solte
    {
        if (isStuck && Input.GetKeyDown(KeyCode.Space))
        {
            numberScape++;

            if (numberScape >= amountNumberScape)
            {
                speed = saveSpeed;
                isStuck = false;
                numberScape = 0;
            }
        }
    }

    void SetPosition()
    {
        if (systemDoor.IsHaking && inPointHacking)
        {
            inPointHacking = false;
            transform.position = new Vector2(transform.position.x - 0.4f, transform.position.y);
        }

        // pode voltar a posição quando termina de hackear
        if (!systemDoor.IsHaking)
        {
            inPointHacking = true;
        }
    }

    void DetectCollider()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
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
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
        Gizmos.DrawWireSphere(pos, radius);
    }

    //collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //game over
        if (collision.collider.CompareTag("Enemy"))
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
            Destroy(gameObject);
        }
    }

    //target
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PontoB"))
        {
            isElevator = false;
        }
        if (collision.CompareTag("Goo") && !gameObject.GetComponent<Hide>().Hidden)
        {
            isStuck = true;
            speed = 0;
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
