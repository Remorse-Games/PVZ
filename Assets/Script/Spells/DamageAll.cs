using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAll : BaseSpell
{
    private Vector2 corner1, corner2;
    public LayerMask enemyLayer;
    public int damage;
    private void Start()
    {
        Camera camera;
        camera = Camera.main;
        corner1 = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        corner2 = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }
    protected override void Spell()
    {
        Collider2D[] colls;
        colls = Physics2D.OverlapAreaAll(corner1, corner2, enemyLayer);
        foreach (Collider2D collider in colls)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(enemy.enemyData.maxHP / 2);
            }
        }
    }
}
