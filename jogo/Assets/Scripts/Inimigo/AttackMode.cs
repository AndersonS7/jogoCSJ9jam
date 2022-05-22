using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMode : MonoBehaviour
{
    [SerializeField] private GameObject goo;
    [SerializeField] private GameObject player;

    private float timeCount;
    //private GameObject target;
    [SerializeField]  private GameObject[] targets;
    private bool isShoot; //controla quando o inimigo pode ou não atirar
    private bool attackT; //indica para o animator quando o inimigo está correndo atrás da armadilha
    private AudioSource songAttack;
    private bool songTrue;

    private Enemy enemy;

    public bool AttackT { get => attackT; }

    // Start is called before the first frame update
    void Start()
    {
        songAttack = transform.GetChild(0).GetComponent<AudioSource>();
        isShoot = false;
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        AttackTrack();

        if (enemy.IsAttack)
        {
            DirectionControl(player.transform);
            Attack();
        }
    }

    public void DirectionControl(Transform obj)
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
            , player.transform.position, 1.5f * Time.deltaTime);

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
        //lista de alvos na cena
        targets = GameObject.FindGameObjectsWithTag("Track");

        foreach (var target in targets)
        {
            //identifica se o inimigo está na mesma linha que a armadilha
            if (target != null 
                && target.name == "active"
                && target.transform.position.y + 2 >= transform.position.y
                && target.transform.position.y - 2 <= transform.position.y)
            {
                attackT = true;
                DirectionControl(target.transform);

                //ativa o som
                if (!songTrue)
                {
                    StartCoroutine("Song");
                    songTrue = true;
                }

                transform.position = Vector3.MoveTowards(transform.position
                , target.transform.position, 2f * Time.deltaTime);
            }
        }

        if (targets.Length <= 0)
        {
            attackT = false;
        }
    }

    IEnumerator Song()
    {
        songAttack.gameObject.SetActive(true);
        songAttack.Play();

        yield return new WaitForSeconds(Random.Range(2f, 3.5f));
        songAttack.gameObject.SetActive(true);
        songAttack.Play();
        songTrue = false;
    }
}
