using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource[] levelMusics;

    private void Start()
    {
        levelMusics = GetComponents<AudioSource>();
    }

    IEnumerator fadeOutMusic(int index)
    {
        for(int i = 0; i < 10; i++)
        {
            levelMusics[index].volume -= 0.4f / 10;
            yield return new WaitForSeconds(0.1f);
        }
        levelMusics[index].volume = 0;
        if (index != 0)
        {
            levelMusics[index].Stop();
        }
    }

    IEnumerator fadeInMusic(int index)
    {
        levelMusics[index].Play();
        levelMusics[index].volume = 0;
        for(int i = 0; i < 10; i++)
        {
            levelMusics[index].volume += 0.4f / 10;
            yield return new WaitForSeconds(0.1f);
        }
        levelMusics[index].volume = 0.4f;
    }

    public void fadeOut(int i)
    {
        StartCoroutine(fadeOutMusic(i));
    }

    public void fadeIn(int i)
    {
        StartCoroutine(fadeInMusic(i));
    }
}
