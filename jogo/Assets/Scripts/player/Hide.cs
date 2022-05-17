using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    [SerializeField] private Color hiddenColor;
    [SerializeField] private Color notHiddenColor;

    private SpriteRenderer spriteRender;
    private Collider2D coll2D;

    private Vector2 coll2DOffset;
    private bool hidden;
    private bool collidedBox;

    public bool Hidden { get => hidden; }

    void Start()
    {
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
        coll2D = gameObject.GetComponent<Collider2D>();
        coll2DOffset = coll2D.offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (collidedBox && Input.GetKeyDown(KeyCode.E))
        {
            if (!hidden)
            {
                hidden = true;
            }
            else if (hidden)
            {
                hidden = false;
            }
        }

        HiddenControl();
    }

    private void HiddenControl()
    {
        if (hidden)
        {
            spriteRender.color = hiddenColor;
            coll2D.offset = new Vector2(0, 1.1f);
        }
        else
        {
            spriteRender.color = notHiddenColor;
            coll2D.offset = coll2DOffset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            collidedBox = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            collidedBox = false;
            hidden = false;
        }
    }

}
