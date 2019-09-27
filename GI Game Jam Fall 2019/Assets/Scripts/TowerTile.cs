using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : Tile
{
    public GameObject tower;

    private void Start()
    {
        setOrderInLayer();
    }
}
