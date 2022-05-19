using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Nave nave;
    private Vector2 target;

    private Vector2 direction;

    void Start()
    {
        nave = FindObjectOfType<Nave>();
        target = nave.MousePos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, 10 * Time.deltaTime);

        if (transform.position.x == target.x)
        {
            Destroy(gameObject);
        }
    }
}
