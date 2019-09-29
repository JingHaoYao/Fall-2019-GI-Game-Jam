using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBullet : TowerProjectile
{
    public GameObject smallBullet;

    void Start()
    {
        for(int i = 0; i < 16; i++)
        {
            float angle = i * 22.5f;
            GameObject bullet = Instantiate(smallBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<TowerProjectile>().damage = damage;
            bullet.GetComponent<TowerProjectile>().speed = speed;
            bullet.GetComponent<TowerProjectile>().direction = angle;
        }
        Destroy(this.gameObject);
    }

}
