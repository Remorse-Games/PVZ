using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMelee : Tower
{
    protected override void Attack(Enemy target)
    {
        if (target != null)
        {
            target.TakeDamage(towerData.currStrength);
        }
    }
}
