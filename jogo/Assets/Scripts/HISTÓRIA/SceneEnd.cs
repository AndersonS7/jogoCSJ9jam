using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SceneEnd : MonoBehaviour
{
    [SerializeField] Image[] stories;
    [SerializeField] Color[] colors;
    [SerializeField] Text txt;

    private float timeCountColor;
    private int indexColor;
    private int indexStory;

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
            indexColor++;

            if (indexStory == 2)
            {
                txt.color = colors[indexColor];
            }
        }
    }
}
