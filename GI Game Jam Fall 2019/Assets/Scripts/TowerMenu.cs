using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    public Image[] imageList;
    public Sprite[] pixelIcons;
    public int numberRedPixels;
    public int numberBluePixels;
    public int numberGreenPixels;
    public WaveSpawner wavespawner;

    int currentTile;

    void Start()
    {
        wavespawner = FindObjectOfType<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addIcon(int whatPixel)
    {
        if ((whatPixel == 1 && wavespawner.numRedPixels > 0) || (whatPixel == 2 && wavespawner.numGreenPixels > 0) || (whatPixel == 3 && wavespawner.numBluePixels > 0)) {
            if (this.gameObject.activeSelf == true)
            {
                imageList[currentTile].enabled = true;
                imageList[currentTile].sprite = pixelIcons[whatPixel - 1];

                switch (whatPixel)
                {
                    case 1:
                        numberRedPixels++;
                        break;
                    case 2:
                        numberGreenPixels++;
                        break;
                    case 3:
                        numberBluePixels++;
                        break;
                }
            }
        }

    }

    private void OnEnable()
    {
        foreach(Image icon in imageList)
        {
            icon.enabled = false;
        }

        numberGreenPixels = 0;
        numberBluePixels = 0;
        numberRedPixels = 0;

        currentTile = 0;
    }
}
