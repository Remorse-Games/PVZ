using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    public float range = 5f;
    protected override Tower FindTargetTower(MovementDirection direction)
    {
        Tower tower;
        Collider2D coll = Physics2D.OverlapCircle(transform.position, range, towerMask);
        if (coll != null)
        {
            tower = coll.GetComponent<Tower>();
        }
        else
        {
            tower = null;
        }
        return tower;
    }
}
