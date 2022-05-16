using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMode : MonoBehaviour
{
    [SerializeField] private GameObject goo;
    [SerializeField] private Transform player;

    private bool isShoot; //controla quando o inimigo pode ou não atirar
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
        if (enemy.IsAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timeCount += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position
            , player.position, 1f * Time.deltaTime);

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
}
