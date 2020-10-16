using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    private bool targetAvailable;
    public delegate void TargetAvailability();
    public event TargetAvailability TargetIsAvailable, TargetIsNotAvailable;
    public void SetRange(float range)
    {
        GetComponent<CircleCollider2D>().radius = range;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.gameObject);
            if (!targetAvailable)
            {
                targetAvailable = true;
                TargetIsAvailable?.Invoke();
            }
        }
    }
    public T GetTarget<T>()
    {
        if (targetAvailable)
        {
            return enemies[0].GetComponent<T>();
        }
        else
        {
            return default;
        }
    }
    public bool IsTargetAvailable()
    {
        return targetAvailable;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Remove(collision.gameObject);
            if(enemies.Count == 0)
            {
                targetAvailable = false;
                TargetIsNotAvailable?.Invoke();
            }
        }
    }
}
