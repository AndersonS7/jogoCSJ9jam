using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private GameObject track;
    [SerializeField] private Transform point;
    [SerializeField] private GameObject[] trakcs;

    private int index; //essa variavel está sendo usada no script da armadilha
    private int totalTrack;

    public int Index { set => index = value; }

    // Start is called before the first frame update
    void Start()
    {
        totalTrack = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && totalTrack > 0)
        {
            Instantiate(track, point.position, Quaternion.identity);
            totalTrack--;

            trakcs = GameObject.FindGameObjectsWithTag("Track");
        }

        if (Input.GetKeyDown(KeyCode.E) && index < trakcs.Length && trakcs.Length > 0)
        {
            if (trakcs[index] != null)
            {
                trakcs[index].name = "active";
                index++;
            }
        }
    }
}
