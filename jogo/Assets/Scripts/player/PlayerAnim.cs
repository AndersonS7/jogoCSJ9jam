using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator anim;

    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        Walking();
        IsHacking();
    }

    private void Walking()
    {
        if (player.Direction.sqrMagnitude > 0)
        {
            anim.SetInteger("num", 1);
        }
        else
        {
            anim.SetInteger("num", 0);
        }
    }

    private void IsHacking()
    {
        if (player.SystemDoor.IsHaking)
        {
            anim.SetInteger("num", 2);
        }
    }
}