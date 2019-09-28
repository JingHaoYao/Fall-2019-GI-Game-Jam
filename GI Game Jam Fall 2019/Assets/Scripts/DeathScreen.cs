using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Text deathText;
    public Button replayButton;
    public Image deathIcon;

    Animator animator;

    WaveSpawner waveSpawner;

    IEnumerator turnOnDeathScreen()
    {
        yield return new WaitForSeconds(1f);
        deathText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);
        deathIcon.gameObject.SetActive(true);
    }

    IEnumerator turnOffDeathScreen()
    {
        animator.SetTrigger("FadeOut");
        deathText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        deathIcon.gameObject.SetActive(false);
        waveSpawner.playerDead();
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    public void replay()
    {
        StartCoroutine(turnOffDeathScreen());
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        StartCoroutine(turnOnDeathScreen());
        deathText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        deathIcon.gameObject.SetActive(false);
    }
}
