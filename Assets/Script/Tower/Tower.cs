using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    public TowerData towerData;
    private Button button;
    private Slider sliderHP;
    private TargetFinder targetFinder;
    private int currHP;
    private Coroutine attacking;
    private Camera mainCam;
    
    private void Start()
    {
        towerData.Init();
        mainCam = Camera.main;
        currHP = towerData.maxHP;
        button = GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = "Upgrade: " + towerData.upgradeCost.ToString() + " gold";
        UnityEngine.Events.UnityAction action;
        action = Upgrade;
        button.onClick.AddListener(action);
        button.gameObject.SetActive(false);
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
        if (towerData.currAgility > 0)
        {
            attacking = StartCoroutine(Attacking(1f / towerData.currAgility));
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
        if (attacking != null)
        {
            StopCoroutine(attacking);
        }
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
    public void Upgrade()
    {
        if (CoinManager.instance.CanPurchase(towerData.upgradeCost))
        {
            CoinManager.instance.Purchase(towerData.upgradeCost);
            towerData.OnUpgrade();
            StopAttacking();
            StartAttacking();
            Destroy(button.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && button != null)
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 currPos = transform.position;
            if (mousePos.x >= currPos.x - .5f && mousePos.x <= currPos.x + .5f && mousePos.y >= currPos.y - .5f && mousePos.y <= currPos.y + .5f)
            {
                button.gameObject.SetActive(true);
            }
        }
        else if(Input.GetMouseButtonUp(0) && button != null)
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 currPos = transform.position;
            if (!(mousePos.x >= currPos.x - .5f && mousePos.x <= currPos.x + .5f && mousePos.y >= currPos.y - 1f && mousePos.y <= currPos.y + .5f))
            {
                button.gameObject.SetActive(false);
            }
        }
    }
}
