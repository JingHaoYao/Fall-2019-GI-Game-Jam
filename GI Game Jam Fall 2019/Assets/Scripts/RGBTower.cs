using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBTower : MonoBehaviour
{
    List<Turret> currentTurretBonuses = new List<Turret>();

    private void OnTriggerStay2D(Collider2D Target)
    {
        if (Target.gameObject.GetComponent<Turret>())
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
}
