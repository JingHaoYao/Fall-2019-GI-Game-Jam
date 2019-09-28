using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : Tile
{
    public GameObject tower;
    public WaveSpawner spawner;


    private void Start()
    {
        spawner = FindObjectOfType<WaveSpawner>();
        setOrderInLayer();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (spawner.towerMenu.activeSelf == false)
            {
                spawner.towerMenu.SetActive(true);
                spawner.towerMenu.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            }
            else
            {
                spawner.towerMenu.SetActive(false);
            }

        }
    }
}
