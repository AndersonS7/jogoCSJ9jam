using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private GameObject track;
    [SerializeField] private Transform point;

    [SerializeField] private GameObject[] trakcs;
    private int indexTrack;
    private int totalTrack;

    // Start is called before the first frame update
    void Start()
    {
        indexTrack = 0;
        totalTrack = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && totalTrack > 0)
        {
            Instantiate(track, point.position, Quaternion.identity);
            totalTrack--;

            trakcs = GameObject.FindGameObjectsWithTag("Track");
        }

        if (Input.GetKeyDown(KeyCode.E) && indexTrack < trakcs.Length && trakcs.Length > 0)
        {
            trakcs[indexTrack].name = "active";
            indexTrack++;
        }

        Debug.Log("total index: " + indexTrack);
    }
}
