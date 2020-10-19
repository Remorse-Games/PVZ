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
    private List<MovementData> path;
    public EnemyData enemyData;
    public Slider sliderHP;
    private int currHP;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currHP = enemyData.maxHP;
        sliderHP.maxValue = enemyData.maxHP;
        sliderHP.value = enemyData.maxHP;
    }
    public void SetPath(List<MovementData> path)
    {
        this.path = path;
        StartCoroutine(Moving());
    }
    IEnumerator Moving()
    {
        int length;
        length = path.Count;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < path[i].length; j++)
            {
                ChangeDirection(path[i].direction);
                transform.position = new Vector3(Mathf.Floor(transform.position.x + 0.5f), Mathf.Floor(transform.position.y + 0.5f), 0);
                yield return new WaitForSeconds(1f);
            }
        }
        yield return null;
    }
    private void ChangeDirection(MovementDirection direction)
    {
        switch (direction)
        {
            case MovementDirection.Up:
                rb.velocity = Vector2.up;
                break;
            case MovementDirection.Left:
                rb.velocity = Vector2.left;
                break;
            case MovementDirection.Right:
                rb.velocity = Vector2.right;
                break;
            case MovementDirection.Down:
                rb.velocity = Vector2.down;
                break;
        }
    }
    public void TakeDamage(int damage)
    {
        currHP -= damage;
        Debug.Log(currHP);
        sliderHP.value = currHP;
        if (currHP <= 0)
        {
            //ded
            Destroy(gameObject);
        }
    }
}
