using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Enemy enemy;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.IsMoving)
        {
            anim.SetInteger("num", 1);
        }
        else
        {
            anim.SetInteger("num", 0);
        }
    }

}
