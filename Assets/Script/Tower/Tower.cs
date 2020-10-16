using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData towerData;
    private TargetFinder targetFinder;
    private int currHP;
    private Coroutine attacking;
    private void Start()
    {
        currHP = towerData.maxHP;
        GetComponent<SpriteRenderer>().sprite = towerData.sprite;
        targetFinder = GetComponentInChildren<TargetFinder>();
        targetFinder.SetRange(towerData.range);
        targetFinder.TargetIsAvailable += StartAttacking;
        targetFinder.TargetIsNotAvailable += StopAttacking;
    }
    private void StartAttacking()
    {
        if (towerData.attackDelay <= 0)
        {
            attacking = StartCoroutine(Attacking(1f));
        }
        else
        {
            attacking = StartCoroutine(Attacking(towerData.attackDelay));
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
        if (currHP <= 0)
        {
            //ded
        }
    }
}
