﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject circleEnemy, triangleEnemy, squareEnemy;
    public WaveInfo targetWaves;
    public int whatWaveCurrent = 0;
    public GameObject button;
    public Text healthText;

    public GameObject[] levelList;
    public GameObject currLevel;
    public int whatLevel = 0;

    public int playerHealth = 1;

    public GameObject deathScreen;

    public GameObject towerMenu;

    public int numRedPixels;
    public int numGreenPixels;
    public int numBluePixels;

    public Text redPixelText;
    public Text greenPixelText;
    public Text bluePixelText;

    public GameObject beatGameScreen;

    int enemyNumber = 0;

    public void updatePixelText()
    {
        redPixelText.text = numRedPixels.ToString();
        greenPixelText.text = numGreenPixels.ToString();
        bluePixelText.text = numBluePixels.ToString();
    }

    private void Start()
    {
        whatLevel = 0;

        currLevel = Instantiate(levelList[whatLevel], Vector3.zero, Quaternion.identity);
        numRedPixels = currLevel.GetComponentInChildren<WaveInfo>().startingRedPixels;
        numGreenPixels = currLevel.GetComponentInChildren<WaveInfo>().startingGreenPixels;
        numBluePixels = currLevel.GetComponentInChildren<WaveInfo>().startingBluePixels;
        targetWaves = currLevel.GetComponentInChildren<WaveInfo>();
        whatWaveCurrent = 0;
        playerHealth = 5;
        setHealth();
        updatePixelText();
    }

    public void turnOnDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void playerDead()
    {
        FindObjectOfType<MusicManager>().fadeOut(whatLevel);
        towerMenu.SetActive(false);
        whatLevel = 0;
        Destroy(currLevel);
        foreach(Enemy enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }

        currLevel = Instantiate(levelList[whatLevel], Vector3.zero, Quaternion.identity);
        numRedPixels = currLevel.GetComponentInChildren<WaveInfo>().startingRedPixels;
        numGreenPixels = currLevel.GetComponentInChildren<WaveInfo>().startingGreenPixels;
        numBluePixels = currLevel.GetComponentInChildren<WaveInfo>().startingBluePixels;
        targetWaves = currLevel.GetComponentInChildren<WaveInfo>();
        whatWaveCurrent = 0;
        playerHealth = 20;
        setHealth();
        updatePixelText();
    }

    public void setHealth()
    {
        healthText.text = playerHealth.ToString();
    }

    IEnumerator spawnWave(int whatWave)
    {
        float waitBetweenEnemies = 0.5f;
        PathTemplate pathTemplates = FindObjectOfType<PathTemplate>();

        for (int i = 0; i < targetWaves.waveList[whatWave].waveGroups.Length; i++)
        {
            for (int k = 0; k < targetWaves.waveList[whatWave].waveGroups[i].numberCircles; k++)
            {
                GameObject enemy = Instantiate(circleEnemy, pathTemplates.startPosition, Quaternion.identity);
                enemy.GetComponent<Enemy>().enemyOrder = enemyNumber;
                enemyNumber++;

                yield return new WaitForSeconds(waitBetweenEnemies);
            }

            for (int k = 0; k < targetWaves.waveList[whatWave].waveGroups[i].numberTriangles; k++)
            {
                GameObject enemy = Instantiate(triangleEnemy, pathTemplates.startPosition, Quaternion.identity);
                enemy.GetComponent<Enemy>().enemyOrder = enemyNumber;
                enemyNumber++;

                yield return new WaitForSeconds(waitBetweenEnemies);
            }

            for (int k = 0; k < targetWaves.waveList[whatWave].waveGroups[i].numberSquares; k++)
            {
                GameObject enemy = Instantiate(squareEnemy, pathTemplates.startPosition, Quaternion.identity);
                enemy.GetComponent<Enemy>().enemyOrder = enemyNumber;
                enemyNumber++;

                yield return new WaitForSeconds(waitBetweenEnemies);
            }

            yield return new WaitForSeconds(targetWaves.waveList[whatWave].waitInterval);
        }

        while (FindObjectsOfType<Enemy>().Length > 0)
        {
            yield return null;
        }

        button.SetActive(true);

        if(whatWave >= targetWaves.waveList.Length - 1)
        {
            Destroy(currLevel);
            whatLevel++;
            if (whatLevel >= levelList.Length)
            {
                beatGameScreen.SetActive(true);
            }
            else
            {
                FindObjectOfType<MusicManager>().fadeOut(whatLevel - 1);
                FindObjectOfType<MusicManager>().fadeIn(whatLevel);
                whatWaveCurrent = 0;
                currLevel = Instantiate(levelList[whatLevel], Vector3.zero, Quaternion.identity);
                numRedPixels = currLevel.GetComponentInChildren<WaveInfo>().startingRedPixels;
                numGreenPixels = currLevel.GetComponentInChildren<WaveInfo>().startingGreenPixels;
                numBluePixels = currLevel.GetComponentInChildren<WaveInfo>().startingBluePixels;
                targetWaves = currLevel.GetComponentInChildren<WaveInfo>();
                currLevel.GetComponentInChildren<WaveInfo>();
                if (FindObjectOfType<TowerMenu>())
                {
                    FindObjectOfType<TowerMenu>().gameObject.SetActive(false);
                }
                updatePixelText();
            }
        }
        else
        {
            for(int i = 0; i < currLevel.GetComponentInChildren<WaveInfo>().numberPixelsToAdd; i++)
            {
                int whatPixel = Random.Range(0, 3);
                if (whatPixel == 0)
                {
                    numRedPixels++;
                }
                else if(whatPixel == 1)
                {
                    numBluePixels++;
                }
                else
                {
                    numGreenPixels++;
                }
            }
            updatePixelText();
        }
    }

    public void startWave()
    {
        StartCoroutine(spawnWave(whatWaveCurrent));
        button.SetActive(false);
        whatWaveCurrent++;
        GetComponents<AudioSource>()[1].Play();
    }
}
