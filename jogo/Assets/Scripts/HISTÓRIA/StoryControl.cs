using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryControl : MonoBehaviour
{
    [SerializeField] Image[] stories;
    [SerializeField] Color[] colors;

    private Text txt;
    private Image img;

    private float timeCountColor;
    private float timeCountStory;
    private int indexColor;
    private int indexStory;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    void Update()
    {
        if (indexColor == 23)
        {
            indexColor = 0;
            indexStory++;
        }

        timeCountColor += Time.deltaTime;

        if (timeCountColor >= 0.1f && indexColor < colors.Length)
        {
            StoryNext();
            timeCountColor = 0;
        }
    }

    private void StoryNext()
    {
        if (indexStory < stories.Length)
        {
            stories[indexStory].color = colors[indexColor];
            txt = stories[indexStory].GetComponentInChildren(typeof(Text)) as Text;
            img = stories[indexStory].transform.GetChild(1).gameObject.GetComponent<Image>();

            txt.color = colors[indexColor];
            img.color = colors[indexColor];

            indexColor++;
        }
    }
}
