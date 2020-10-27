using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Data", menuName = "Tower Data")]
public class TowerData : ScriptableObject
{
    public int maxHP = 100;
    public float range = 1;
    public int strength, magic, agility = 1;
    public Sprite sprite;
    public bool isRanged;
    public GameObject projectile;
}
