using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : Tile
{
    public PathTile nextTile, prevTile;

    void Start()
    {
        setOrderInLayer();
    }
}
