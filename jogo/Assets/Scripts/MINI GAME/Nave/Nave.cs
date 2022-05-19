using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    private Vector2 pos;
    private Vector2 mousePos;

    public Vector2 MousePos { get => mousePos; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Shot();
    }

    private void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FollowMouse();
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    private void FollowMouse()
    {
        pos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(pos);

        transform.rotation = Quaternion.EulerAngles(mousePos);
    }
}
