using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRanged : Tower
{
    protected override void Attack(Enemy target)
    {
        Projectile projectile = Instantiate(towerData.projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetTarget(target, towerData.currStrength, towerData.currMagic);
    }
}
