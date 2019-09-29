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
    public TowerList TowerList;
    public TowerTile targetTile;

    public GameObject particles;
    public GameObject Turret;

    public GameObject destroyButton;

    int currentTile;

    void Start()
    {
        wavespawner = FindObjectOfType<WaveSpawner>();
        TowerList = FindObjectOfType<TowerList>();
    }

    public void addIcon(int whatPixel)
    {
        if (((whatPixel == 1 && wavespawner.numRedPixels > 0) || (whatPixel == 2 && wavespawner.numGreenPixels > 0) || (whatPixel == 3 && wavespawner.numBluePixels > 0) && currentTile < 3) && targetTile.tower == null) {
            if (this.gameObject.activeSelf == true)
            {
                imageList[currentTile].enabled = true;
                imageList[currentTile].sprite = pixelIcons[whatPixel - 1];

                switch (whatPixel)
                {
                    case 1:
                        numberRedPixels++;
                        wavespawner.numRedPixels--;
                        break;
                    case 2:
                        numberGreenPixels++;
                        wavespawner.numGreenPixels--;
                        break;
                    case 3:
                        numberBluePixels++;
                        wavespawner.numBluePixels--;
                        break;
                }
            }
            currentTile++;
        }

        wavespawner.updatePixelText();

        if(currentTile == 3)
        {
            StartCoroutine(craft());
            targetTile.clicked = false;
            if (numberBluePixels == 3)
            {
                targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[0];
            }
            else if (numberRedPixels == 3)
            {
                targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[2];
            }
            else if (numberGreenPixels == 3)
            {
                targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[1];
            }
            else if (numberGreenPixels == 1 && numberRedPixels == 1 && numberBluePixels == 1)
            {
                targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[3];
            }
            else
            {
                if (numberRedPixels == 2)
                {
                    if (numberGreenPixels == 1)
                    {
                        targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[5];
                    }
                    else
                    {
                        targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[6];
                    }
                }
                else if (numberGreenPixels == 2)
                {
                    if(numberRedPixels == 1)
                    {
                        targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[4];
                    }
                    else
                    {
                        targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[9];
                    }
                }
                else if (numberBluePixels == 2)
                {
                    if (numberRedPixels == 1)
                    {
                        targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[8];
                    }
                    else
                    {
                        targetTile.tower = TowerList.GetComponent<TowerList>().Turrets[7];
                    }
                }
            }
        }
    }

    IEnumerator craft()
    {
        for(int i = 0; i < 10; i++)
        {
            foreach(Image image in imageList)
            {
                image.color = new Color(1, 1, 1, 1 - 0.1f*i);
            }
            yield return new WaitForSeconds(.05f);
        }
        Turret = Instantiate(targetTile.tower);
        Turret.transform.parent = targetTile.transform;
        Turret.transform.localPosition = new Vector3(0, 0, 0);
        Turret.transform.localScale = new Vector3(1, 1, 1);
        Instantiate(particles, targetTile.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {

        StartCoroutine(waitForTowerAssignment());
    }

    IEnumerator waitForTowerAssignment()
    {
        while(targetTile == null)
        {
            yield return null;
        }

        if (targetTile.tower == null)
        {

            destroyButton.SetActive(false);

            foreach (Image icon in imageList)
            {
                icon.transform.parent.gameObject.SetActive(true);
                icon.enabled = false;
            }

            numberGreenPixels = 0;
            numberBluePixels = 0;
            numberRedPixels = 0;

            currentTile = 0;

            foreach (Image image in imageList)
            {
                image.color = Color.white;
            }
        }
        else
        {
            foreach(Image icon in imageList)
            {
                icon.transform.parent.gameObject.SetActive(false);
            }

            destroyButton.SetActive(true);
        }
    }

    public void destroyTower()
    {
        if (targetTile.tower.GetComponent<Turret>())
        {
            Turret turret = targetTile.tower.GetComponent<Turret>();
            List<int> pixelList = new List<int>();

            for(int i = 0; i < turret.numRedPixels; i++)
            {
                pixelList.Add(1);
            }

            for (int i = 0; i < turret.numGreenPixels; i++)
            {
                pixelList.Add(2);
            }

            for (int i = 0; i < turret.numBluePixels; i++)
            {
                pixelList.Add(3);
            }

            for(int i = 0; i < 2; i++)
            {
                int returnPixel = pixelList[Random.Range(0, pixelList.Count)];
                pixelList.Remove(returnPixel);
                if(returnPixel == 1)
                {
                    wavespawner.numRedPixels++;
                }
                else if(returnPixel == 2)
                {
                    wavespawner.numGreenPixels++;
                }
                else
                {
                    wavespawner.numBluePixels++;
                }
            }
        }
        else
        {
            for(int i = 0; i < 2; i++)
            {
                int returnPixel = Random.Range(1, 4);
                if (returnPixel == 1)
                {
                    wavespawner.numRedPixels++;
                }
                else if (returnPixel == 2)
                {
                    wavespawner.numGreenPixels++;
                }
                else
                {
                    wavespawner.numBluePixels++;
                }
            }
        }
        Destroy(targetTile.tower);
        wavespawner.updatePixelText();
        Instantiate(particles, targetTile.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if(currentTile < 3)
        {
            wavespawner.numRedPixels += numberRedPixels;
            wavespawner.numBluePixels += numberBluePixels;
            wavespawner.numGreenPixels += numberGreenPixels;
            wavespawner.updatePixelText();
        }
        currentTile = 0;
    }
}
