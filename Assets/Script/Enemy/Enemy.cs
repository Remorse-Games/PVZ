using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [Serializable]
    public enum MovementDirection
    {
        Up,
        Left,
        Right,
        Down
    };
    [Serializable]
    public struct MovementData
    {
        public MovementDirection direction;
        public int length;
    };
    [SerializeField]
    private LayerMask towerMask;
    private List<MovementData> path;
    public EnemyData enemyData;
    public Slider sliderHP;
    private int currHP;
    private Rigidbody2D rb;
    private Vector2 offset;
    private EnemySpawner spawner;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        offset = new Vector2(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f));
    }
    public void SetEnemy(EnemyData data, List<MovementData> path, EnemySpawner spawner)
    {
        SetData(data);
        SetPath(path);
        animator.runtimeAnimatorController = enemyData.animatorController;
        this.spawner = spawner;
    }
    private void SetData(EnemyData data)
    {
        enemyData = data;
        GetComponent<SpriteRenderer>().sprite = enemyData.sprite;
        currHP = enemyData.maxHP;
        sliderHP.maxValue = enemyData.maxHP;
        sliderHP.value = enemyData.maxHP;
    }
    public void SetPath(List<MovementData> path)
    {
        this.path = path;
        StartCoroutine(Moving());
    }
    private IEnumerator Moving()
    {
        int length;
        length = path.Count;
        Tower tower;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < path[i].length; j++)
            {
                rb.velocity = Vector2.zero;
                tower = FindTargetTower(path[i].direction);
                while (tower != null)
                {
                    animator.SetTrigger("Attack");
                    animator.SetBool("Attacking", true);
                    tower.TakeDamage(enemyData.strength);
                    yield return new WaitForSeconds((enemyData.agility <= 0) ? 1f : 1f / enemyData.agility);
                }
                animator.ResetTrigger("Attack");
                animator.SetBool("Attacking", false);
                ChangeDirection(path[i].direction);
                transform.position = new Vector3(Mathf.Floor(transform.position.x + 0.5f) + offset.x, Mathf.Floor(transform.position.y + 0.5f) + offset.y, 0);
                yield return new WaitForSeconds(1f);
            }
        }
        yield return null;
    }
    private Tower FindTargetTower(MovementDirection direction)
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
    private void ChangeDirection(MovementDirection direction)
    {
        switch (direction)
        {
            case MovementDirection.Up:
                rb.velocity = Vector2.up * enemyData.speed;
                break;
            case MovementDirection.Left:
                rb.velocity = Vector2.left * enemyData.speed;
                break;
            case MovementDirection.Right:
                rb.velocity = Vector2.right * enemyData.speed;
                break;
            case MovementDirection.Down:
                rb.velocity = Vector2.down * enemyData.speed;
                break;
        }
    }
    public void TakeDamage(int damage)
    {
        currHP -= damage;
        //Debug.Log(currHP);
        sliderHP.value = currHP;
        if (currHP <= 0)
        {
            OnDead();
        }
    }
    private void OnDead()
    {
        CoinManager.instance.Earn(enemyData.money);
        spawner.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
