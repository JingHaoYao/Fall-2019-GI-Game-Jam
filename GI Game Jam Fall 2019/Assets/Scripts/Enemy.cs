﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    Rigidbody2D rigidbody;

    PathTemplate path_template;
    Path path_to_follow;
    PathTile target_tile;
    SpriteRenderer spriteRenderer;
    Animator animator;

    WaveSpawner waveSpawner;
    public GameObject deathParticles;

    public int enemyOrder = 0;

    public int whatPathToFollow = 0;

    int currTileReached = 0;
 
    void Start()
    {
        path_template = FindObjectOfType<PathTemplate>();
        rigidbody = GetComponent<Rigidbody2D>();
        int whatPath = Mathf.Clamp(whatPathToFollow, 0, path_template.paths.Length - 1);
        path_to_follow = path_template.paths[whatPath];
        target_tile = path_to_follow.path[0];
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Update()
    {
        if (target_tile != null)
        {
            if (health > 0)
            {
                float angle = Mathf.Atan2(
                    target_tile.transform.position.y - transform.position.y,
                    target_tile.transform.position.x - transform.position.x
                );
                rigidbody.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
                if (Vector2.Distance(transform.position, target_tile.transform.position) > 0.1f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg);
                }

                if (Vector2.Distance(transform.position, target_tile.transform.position) < 0.05f)
                {
                    currTileReached++;
                    if (currTileReached < path_to_follow.path.Length)
                    {
                        target_tile = path_to_follow.path[currTileReached];
                    }
                    else
                    {
                        target_tile = null;
                    }
                }
            }
            else
            {
                rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            waveSpawner.playerHealth--;
            waveSpawner.GetComponents<AudioSource>()[0].Play();
            if(waveSpawner.playerHealth <= 0)
            {
                waveSpawner.turnOnDeathScreen();
            }

            waveSpawner.setHealth();
            Destroy(this.gameObject);
            rigidbody.velocity = Vector2.zero;
        }
    }


    IEnumerator hitFrame()
    {
        for(int i = 0; i < 2; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TowerProjectile>() && health > 0)
        {
            health -= collision.gameObject.GetComponent<TowerProjectile>().damage;
            StartCoroutine(hitFrame());

            if (health <= 0)
            {
                this.GetComponent<Collider2D>().enabled = false;
                //kill thing
                animator.SetTrigger("Death");
                GetComponents<AudioSource>()[0].Play();
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                Destroy(gameObject, 1f);
            }
            GetComponents<AudioSource>()[1].Play();
        }
    }
}
