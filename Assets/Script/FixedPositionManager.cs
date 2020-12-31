using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPositionManager : MonoBehaviour
{
    public static FixedPositionManager instance;
    private Dictionary<Vector2Int, bool> fixedTowers;
    public List<Vector2Int> fixedPos;
    private void Start()
    {
        instance = this;
        fixedTowers = new Dictionary<Vector2Int, bool>();
        foreach (Vector2Int pos in fixedPos)
        {
            fixedTowers.Add(pos, false);
        }
    }
    public bool CanPlace(Vector2Int pos)
    {
        if (!fixedTowers.ContainsKey(pos)) return false;
        return !fixedTowers[pos];
    }
    public void Place(Vector2Int pos)
    {
        if (!fixedTowers.ContainsKey(pos)) return;
        fixedTowers[pos] = true;
    }
    public void Remove(Vector2Int pos)
    {
        if (!fixedTowers.ContainsKey(pos)) return;
        fixedTowers[pos] = false;
    }
}
