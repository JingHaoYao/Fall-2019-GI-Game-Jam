using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBTower : MonoBehaviour
{
    List<Turret> currentTurretBonuses = new List<Turret>();
    public float rangeValue;

    private void Start()
    {
        CircleCollider2D Range = GetComponent<CircleCollider2D>();
        Range.radius = rangeValue;
    }

    private void OnTriggerEnter2D(Collider2D Target)
    {
        if (Target.gameObject.GetComponent<Turret>() && Vector2.Distance(transform.position, Target.gameObject.transform.position) < 3.2f)
        {
            if (!currentTurretBonuses.Contains(Target.gameObject.GetComponent<Turret>()))
            {
                currentTurretBonuses.Add(Target.gameObject.GetComponent<Turret>());
                Target.gameObject.GetComponent<Turret>().ReloadTime *= 0.75f;
                Target.gameObject.GetComponent<Turret>().BulletDamage += 1;
            }
        }
    }

    private void OnDestroy()
    {
        foreach(Turret turret in currentTurretBonuses)
        {
            turret.ReloadTime /= 0.75f;
            turret.BulletDamage -= 1;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3.2f);
    }*/
}
