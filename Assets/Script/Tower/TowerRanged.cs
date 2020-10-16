using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRanged : Tower
{
    [SerializeField]
    private GameObject projectilePrefab;
    protected override void Attack(Enemy target)
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetTarget(target, towerData.strength, towerData.magic);
    }
}
