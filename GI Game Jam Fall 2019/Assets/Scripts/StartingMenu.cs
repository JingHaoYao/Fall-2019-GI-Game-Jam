using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingMenu : MonoBehaviour
{
    public void turnOffMenu()
    {
        FindObjectOfType<MusicManager>().fadeIn(0);
        this.gameObject.SetActive(false);
    }
}
