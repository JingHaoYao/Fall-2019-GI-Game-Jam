using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 pos = new Vector2(0, 0);
    public Sprite tileSprite;
    public int orderInLayer;

    public void setOrderInLayer()
    {
        GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
    }

    private void Start()
    {
        setOrderInLayer();
    }
}
