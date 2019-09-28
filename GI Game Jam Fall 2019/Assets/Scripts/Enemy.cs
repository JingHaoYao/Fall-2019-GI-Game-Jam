using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int speed;
    public int what_path;
    Rigidbody2D rigidbody;

    PathTemplate path_template;
    Path path_to_follow;
    PathTile target_tile;
  
    void Start()
    {
        path_template = FindObjectOfType<PathTemplate>();
        rigidbody = GetComponent<Rigidbody2D>();
        path_to_follow = path_template.paths[0];
        target_tile = path_to_follow.path[0];
    }

    void Update()
    {
        if (target_tile != null)
        {
            float angle = Mathf.Atan2(
                target_tile.transform.position.y - transform.position.y,
                target_tile.transform.position.x - transform.position.x
            );
            rigidbody.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;

            if (Vector2.Distance(transform.position, target_tile.transform.position) < 0.05f)
            {
                target_tile = target_tile.nextTile;
            }
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
