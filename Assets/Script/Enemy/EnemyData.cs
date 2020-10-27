using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int maxHP = 100;
    public float range = 1;
    public int strength, magic, agility = 1;
    public Sprite sprite;
    public int money = 2;
}
