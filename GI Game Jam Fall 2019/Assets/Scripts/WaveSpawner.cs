﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject circleEnemy, triangleEnemy, squareEnemy;
    public WaveInfo targetWaves;
    int whatWave = 0;
    public GameObject button;
    public Text healthText;

    public GameObject[] levelList;
    public GameObject currLevel;
    public int whatLevel = 0;

    public int playerHealth = 1;

    public GameObject deathScreen;


    private void Start()
    {
        setHealth();
    }

    public void turnOnDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void playerDead()
    {
        whatLevel = 0;
        Destroy(currLevel);
        foreach(Enemy enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }

        currLevel = Instantiate(levelList[whatLevel], Vector3.zero, Quaternion.identity);
        whatWave = 0;
        playerHealth = 20;
        setHealth();
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
                Instantiate(circleEnemy, pathTemplates.startPosition, Quaternion.identity);

                yield return new WaitForSeconds(waitBetweenEnemies);
            }

            for (int k = 0; k < targetWaves.waveList[whatWave].waveGroups[i].numberTriangles; k++)
            {
                Instantiate(triangleEnemy, pathTemplates.startPosition, Quaternion.identity);

                yield return new WaitForSeconds(waitBetweenEnemies);
            }

            for (int k = 0; k < targetWaves.waveList[whatWave].waveGroups[i].numberSquares; k++)
            {
                Instantiate(squareEnemy, pathTemplates.startPosition, Quaternion.identity);

                yield return new WaitForSeconds(waitBetweenEnemies);
            }

            yield return new WaitForSeconds(targetWaves.waveList[whatWave].waitInterval);
        }

        while (FindObjectsOfType<Enemy>().Length > 0)
        {
            yield return null;
        }

        button.SetActive(true);

        if(whatWave >= targetWaves.waveList.Length)
        {
            Destroy(currLevel);
            whatLevel++;
            currLevel = Instantiate(levelList[whatLevel], Vector3.zero, Quaternion.identity);
            whatWave = 0;
        }
    }

    public void startWave()
    {
        StartCoroutine(spawnWave(whatWave));
        button.SetActive(false);
        whatWave++;
    }
}
