using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData towerData;
    private int currHP;
    private void Start()
    {
        currHP = towerData.maxHP;
        GetComponent<SpriteRenderer>().sprite = towerData.sprite;
    }
    public void TakeDamage(int damage)
    {
        currHP -= damage;
        if (currHP <= 0)
        {
            //ded
        }
    }
}
