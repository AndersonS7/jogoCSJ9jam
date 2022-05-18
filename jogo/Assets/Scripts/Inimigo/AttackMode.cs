using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMode : MonoBehaviour
{
    [SerializeField] private GameObject goo;
    [SerializeField] private Transform player;

    private GameObject target;
    private bool isShoot; //controla quando o inimigo pode ou n�o atirar
    private Enemy enemy;

    private float timeCount;

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
            Attack();
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
            transform.position = Vector3.MoveTowards(transform.position
            , target.transform.position, 2f * Time.deltaTime);
        }
    }
}
