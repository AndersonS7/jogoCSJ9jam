using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMode : MonoBehaviour
{
    [SerializeField] private GameObject goo;
    [SerializeField] private Transform player;

    private float timeCount;
    private GameObject target;
    private bool isShoot; //controla quando o inimigo pode ou não atirar
    private bool attackT; //indica para o animator quando o inimigo está correndo atrás da armadilha

    private Enemy enemy;

    public bool AttackT { get => attackT; }

    // Start is called before the first frame update
    void Start()
    {
        isShoot = false;
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackTrack();
        
        if (enemy.IsAttack)
        {
            DirectionControl(player);
            Attack();
        }
    }

    public void DirectionControl(Transform obj) // 0 => player || 1 => outros
    {
        if (obj.position.x - transform.position.x > 0)
        {
            enemy.Direction = 1;
        }

        if (obj.position.x - transform.position.x < 0)
        {
            enemy.Direction = -1;
        }
    }

    private void Attack()
    {
        timeCount += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position
            , player.position, 1.5f * Time.deltaTime);

        if (timeCount >= Random.Range(4, 6))
        {
            isShoot = false;
            timeCount = 0;
        }

        if (!isShoot)
        {
            Instantiate(goo, transform.position, Quaternion.identity);
            isShoot = true;
        }
    }

    private void AttackTrack()
    {
        target = GameObject.Find("active");
        
        if (target != null)
        {
            attackT = true;
            DirectionControl(target.transform);

            transform.position = Vector3.MoveTowards(transform.position
            , target.transform.position, 2f * Time.deltaTime);
        }
        else
        {
            attackT = false;
        }
    }
}
