using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Data", menuName = "Tower Data")]
public class TowerData : ScriptableObject
{
    public int maxHP = 100;
    public float range = 1;
    public int strength, magic, agility = 1;
    [HideInInspector]
    public int currStrength, currMagic, currAgility;
    public Sprite sprite;
    public bool isRanged;
    public GameObject projectile;
    public int upgradeCost;
    public float cooldown;
    public AudioClip spawnSfx;
    public void Init()
    {
        currAgility = agility;
        currMagic = magic;
        currStrength = strength;
    }
    public void OnUpgrade()
    {
        currAgility += 10;
        currStrength += 10;
    }
}
