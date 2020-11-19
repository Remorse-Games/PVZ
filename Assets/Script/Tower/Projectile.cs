using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy target;
    private bool attacking;
    private Rigidbody2D rb;
    public float speed;
    private int strength;//, magic;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 100f);
    }
    public void SetTarget(Enemy enemy, int str, int mgc)
    {
        target = enemy;
        attacking = true;
        strength = str;
        //magic = mgc;
    }
    private void Update()
    {
        if (attacking)
        {
            if (target == null)
            {
                attacking = false;
                return;
            }
            rb.velocity = (Vector2)(target.transform.position - transform.position).normalized * speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attacking && target != null)
        {
            if (collision.gameObject.Equals(target.gameObject))
            {
                target.TakeDamage(strength);
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().TakeDamage(strength);
                Destroy(gameObject);
            }
        }
    }
}
