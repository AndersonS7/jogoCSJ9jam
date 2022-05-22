using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRight : MonoBehaviour
{
    //cores
    [SerializeField] private Color[] colors = new Color[4];

    private UIController uiController;
    private Frog frog;

    void Start()
    {
        UpdateColor();
        uiController = FindObjectOfType<UIController>();
        frog = FindObjectOfType<Frog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!uiController.GameOverBool && !frog.Finish)
        {
            transform.Translate(Vector2.right * 2 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
    }
}
