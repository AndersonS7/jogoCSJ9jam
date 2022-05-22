using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator anim;

    private Player player;
    private Hacking hack;
    private FeedBack feedBack;

    void Start()
    {
        feedBack = FindObjectOfType<FeedBack>();
        player = GetComponent<Player>();
        hack = GetComponent<Hacking>();
    }

    void Update()
    {
        Walking();
        Stopped();
        Hack();
    }

    private void Stopped()
    {
        if (player.IsElevator)
        {
            anim.SetInteger("num", 0);
        }

        if (player.IsStuck)
        {
            anim.SetInteger("num", 0);
        }
    }
    private void Walking()
    {
        if (player.Direction.sqrMagnitude > 0 && !feedBack.Paused)
        {
            anim.SetInteger("num", 1);
        }
        else
        {
            anim.SetInteger("num", 0);
        }
    }

    private void Hack()
    {
        if (hack.Hack)
        {
            anim.SetInteger("num", 2);
        }
    }
}
