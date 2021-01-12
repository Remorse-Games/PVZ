using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseManager : MonoBehaviour
{
    public int hp = 10;
    public Text hpText;
    private void Start()
    {
        UpdateHP();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().RemoveUnit();
            hp--;
            UpdateHP();
            if (hp <= 0)
            {
                //gaemover
                Time.timeScale = 0f;
            }
        }
    }
    private void UpdateHP()
    {
        hpText.text = hp.ToString();
    }
}
