using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Data", menuName = "Tower Data")]
public class TowerData : ScriptableObject
{
    public int maxHP;
    public float range, attackDelay;
    public int strength, magic, agility;
    public Sprite sprite;
    public bool isRanged;
}
