using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : Tile
{
    public GameObject tower;
    public WaveSpawner spawner;
    public bool clicked = false;
    GameObject rangeMeter;


    private void Start()
    {
        spawner = FindObjectOfType<WaveSpawner>();
        setOrderInLayer();
        rangeMeter = GameObject.Find("Range Indicator");
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
        if (tower != null)
        {
            rangeMeter.transform.position = transform.position;

            if (tower.GetComponent<Turret>())
            {
                float range = tower.GetComponent<Turret>().RangeValue;
                rangeMeter.transform.localScale = new Vector3(range *18, range * 18);
            }
            else if (tower.GetComponent<RGBTower>())
            {
                rangeMeter.transform.localScale = new Vector3(tower.GetComponent<RGBTower>().rangeValue * 18, tower.GetComponent<RGBTower>().rangeValue * 18);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (clicked == false)
            {
                StartCoroutine(click());
                spawner.towerMenu.SetActive(true);

                spawner.towerMenu.GetComponent<TowerMenu>().targetTile = this;
                spawner.towerMenu.GetComponent<TowerMenu>().towerAssignment();
                spawner.towerMenu.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                clicked = true;
                foreach(TowerTile tile in FindObjectsOfType<TowerTile>())
                {
                    if(tile != this)
                        tile.clicked = false;
                }
            }
            else
            {
                clicked = false;
                StartCoroutine(click());
                spawner.towerMenu.SetActive(false);
            }
        }
    }

    void OnMouseExit()
    {
        rangeMeter.transform.localScale = new Vector3(0, 0);
    }

    private void OnDestroy()
    {
        rangeMeter.transform.position = new Vector3(-100, 0);
    }
}
