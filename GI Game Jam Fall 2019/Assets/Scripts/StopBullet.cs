using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBullet : TowerProjectile
{
    public float range = 0.6f;
    Vector3 startPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Destroy(this.gameObject);
        }
    }

    new Rigidbody2D rigidbody;

    private void Start()
    {
        startPos = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Vector2.Distance(Camera.main.transform.position, transform.position) > 20 || (Vector2.Distance(startPos, transform.position) > range))
        {
            Destroy(this.gameObject);
        }
        rigidbody.velocity = new Vector3(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad)) * speed;
    }
}
