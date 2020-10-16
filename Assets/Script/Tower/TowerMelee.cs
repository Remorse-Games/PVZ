using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMelee : Tower
{
    protected override void Attack(Enemy target)
    {
        target.TakeDamage(towerData.strength);
    }
}
