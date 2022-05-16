using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private float speed;

    //CONTROLE DE AÇÕES [ANDAR]
    private bool isDramSeg; //sorteia os segundos de espera para o inimigo andar
    private float segMoving; //segundos que o inimigo deve esperar para se mover
    private bool isMoving; //informa quando o inimigo pode andar
    private float timeCount; //vai controlar o sorteio de quando o inimigo vai andar

    //CONTROLE DE AÇÕES [ATACAR]
    private bool isAttack;
    public bool IsAttack { get => isAttack; }

    //CONTROLE DE DIREÇÃO
    private int directionAux;
    private int[] directions;

    void Start()
    {
        directions = new int[] { -1, 1 };
        isMoving = true;
        directionAux = 1;
        isDramSeg = true;
    }

    void Update()
    {
        if (!isAttack)
        {
            IsMovingControl();
            ToMove();
        }
    }

    private void ToMove()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void Direction(int direction)
    {
        if (direction == 1)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (direction == -1)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void IsMovingControl()
    {
        timeCount += Time.deltaTime;

        if (isDramSeg)
        {
            segMoving = Random.Range(3, 5);
            isDramSeg = false;
        }

        if (timeCount >= segMoving)
        {
            directionAux = directions[Random.Range(0, directions.Length)];
            Direction(directionAux);

            if (isMoving)
            {
                isMoving = false;
            }
            else if (!isMoving)
            {
                isMoving = true;
            }

            timeCount = 0;
            isDramSeg = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //quando bater na parede
        if (collision.collider.CompareTag("Wall"))
        {
            if (directionAux == 1)
            {
                Direction(-1);
                directionAux = -1;
            }
            else if (directionAux == -1)
            {
                Direction(1);
                directionAux = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttack = false;
        }
    }
}
