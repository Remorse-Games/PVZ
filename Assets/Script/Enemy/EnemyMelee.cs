using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    protected override Tower FindTargetTower(MovementDirection direction)
    {
        Tower tower;
        Vector2 vector2;
        vector2 = Vector2.zero;
        switch (direction)
        {
            case MovementDirection.Up:
                vector2 = Vector2.up;
                break;
            case MovementDirection.Left:
                vector2 = Vector2.left;
                break;
            case MovementDirection.Right:
                vector2 = Vector2.right;
                break;
            case MovementDirection.Down:
                vector2 = Vector2.down;
                break;
        }
        Collider2D coll = Physics2D.OverlapCircle((Vector2)transform.position + vector2, 0.2f, towerMask);
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
