using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private bool hacking; //se foi hackeado a porta abre
    private bool isUp; //evita do valor ficar sendo sempre icrementado
    private float timeCount;

    // Update is called once per frame
    void Update()
    {
        if (hacking)
        {
            if (timeCount > 1.8f)
            {
                isUp = true;
            }

            if (!isUp)
            {
                timeCount += Time.deltaTime;
            }
            
            if (timeCount < 1.8f)
            {
                door.transform.Translate(Vector2.up * 1.5f * Time.deltaTime);
            }
        }
    }
}
