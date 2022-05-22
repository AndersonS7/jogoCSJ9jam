using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DisabledSong");
    }

    IEnumerator DisabledSong()
    {
        yield return new WaitForSeconds(1.3f);
        gameObject.SetActive(false);
    }
}
