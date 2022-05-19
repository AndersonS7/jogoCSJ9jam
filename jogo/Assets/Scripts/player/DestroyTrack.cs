using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrack : MonoBehaviour
{
    private Track track;

    void Start()
    {
        track = FindObjectOfType<Track>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            track.Index = 0;

            Destroy(gameObject);
        }
    }
}
