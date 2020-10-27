﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public TowerData towerData;
    private Slider sliderHP;
    private TargetFinder targetFinder;
    private int currHP;
    private Coroutine attacking;
    private void Start()
    {
        currHP = towerData.maxHP;
        sliderHP = GetComponentInChildren<Slider>();
        sliderHP.maxValue = towerData.maxHP;
        sliderHP.value = towerData.maxHP;
        GetComponent<SpriteRenderer>().sprite = towerData.sprite;
        targetFinder = GetComponentInChildren<TargetFinder>();
        targetFinder.SetRange(towerData.range);
        targetFinder.TargetIsAvailable += StartAttacking;
        targetFinder.TargetIsNotAvailable += StopAttacking;
    }
    private void StartAttacking()
    {
        if (towerData.agility > 0)
        {
            attacking = StartCoroutine(Attacking(1f / towerData.agility));
        }
    }
    private IEnumerator Attacking(float delayTime)
    {
        while (true)
        {
            Attack(targetFinder.GetTarget<Enemy>());
            yield return new WaitForSeconds(delayTime);
        }
    }
    protected virtual void Attack(Enemy target) { }
    private void StopAttacking()
    {
        StopCoroutine(attacking);
    }
    public void TakeDamage(int damage)
    {
        currHP -= damage;
        sliderHP.value = currHP;
        if (currHP <= 0)
        {
            //ded
            Destroy(gameObject);
        }
    }
}
