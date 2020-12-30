using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    public int hp = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().RemoveUnit();
            hp--;
            if (hp <= 0)
            {
                //gaemover
                Time.timeScale = 0f;
            }
        }
    }
}
