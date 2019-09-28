using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : Tile
{
    public GameObject tower;
    public WaveSpawner spawner;
    public bool clicked = false;


    private void Start()
    {
        spawner = FindObjectOfType<WaveSpawner>();
        setOrderInLayer();
    }

    IEnumerator click()
    {
        SpriteRenderer SR = GetComponent<SpriteRenderer>();

        for(int i = 0; i < 2; i++)
        {
            SR.color = new Color(0.7f, 0.7f, 0.7f, 1);
            yield return new WaitForSeconds(0.1f);
            SR.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && tower == null)
        {
            if (clicked == false)
            {
                StartCoroutine(click());
                spawner.towerMenu.SetActive(true);
                spawner.towerMenu.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                clicked = true;
                foreach(TowerTile tile in FindObjectsOfType<TowerTile>())
                {
                    if(tile != this)
                        tile.clicked = false;
                }

                spawner.towerMenu.GetComponent<TowerMenu>().targetTile = this;
            }
            else
            {
                clicked = false;
                StartCoroutine(click());
                spawner.towerMenu.SetActive(false);
            }
        }
    }
}
