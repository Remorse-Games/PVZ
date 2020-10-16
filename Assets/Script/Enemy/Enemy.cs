using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public Slider sliderHP;
    private int currHP;
    private void Start()
    {
        currHP = enemyData.maxHP;
        sliderHP.maxValue = enemyData.maxHP;
        sliderHP.value = enemyData.maxHP;
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
