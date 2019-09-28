using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float ReloadTime; //Reference values, usually constant
    public float RangeValue;
    public int BulletDamage;
    public GameObject BulletType;
    public float BulletSpeed;
    CircleCollider2D Range;

    public float Reloading; //Active variables
    public GameObject Bullet;
    public int LowestTarget = 99999;

    public float waitTimeTillFire = 0;

    GameObject currentTarget;

    private void Start()
    {
        Range = GetComponent<CircleCollider2D>();
        Range.radius = RangeValue;
    }
    
    IEnumerator fireBullet(float angle)
    {
        GetComponent<Animator>().SetTrigger("Fire");
        yield return new WaitForSeconds(waitTimeTillFire);
        Bullet = Instantiate(BulletType);
        Bullet.transform.rotation = transform.rotation;
        Bullet.GetComponent<TowerProjectile>().damage = BulletDamage;
        Bullet.GetComponent<TowerProjectile>().speed = BulletSpeed;
        Bullet.GetComponent<TowerProjectile>().direction = angle;
        Bullet.transform.position = transform.position;
    }

    private void Update()
    {
        if (currentTarget)
        {
            float angle = Mathf.Atan2(currentTarget.transform.position.y - transform.position.y, currentTarget.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            if (Reloading <= 0)
            {
                StartCoroutine(fireBullet(angle));
                Reloading = ReloadTime;
            }
            else
            {
                Reloading -= Time.deltaTime;
            }
        }
        else
        {
            LowestTarget = 99999;
        }
    }

    private void OnTriggerStay2D(Collider2D Target)
    {
        if (Target.gameObject.GetComponent<Enemy>())
        {
            if (Target.gameObject.GetComponent<Enemy>().enemyOrder <= LowestTarget)
            {
                LowestTarget = Target.gameObject.GetComponent<Enemy>().enemyOrder;
                currentTarget = Target.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == currentTarget)
        {
            currentTarget = null;
            LowestTarget = 99999;
        }
    } 
}
