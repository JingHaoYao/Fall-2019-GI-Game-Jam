using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSprayBullet : TowerProjectile
{
    public GameObject smallBullet;
    public float angleDiff = 15;

    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            float angle = direction - angleDiff + angleDiff * i;
            GameObject bullet = Instantiate(smallBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<TowerProjectile>().damage = damage;
            bullet.GetComponent<TowerProjectile>().speed = speed;
            bullet.GetComponent<TowerProjectile>().direction = angle;
        }
    }
}
