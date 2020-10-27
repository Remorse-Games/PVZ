using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private int coin = 0;
    public Text coinText;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UpdateCoin();
    }
    public int GetCoinAmount()
    {
        return coin;
    }
    public bool CanPurchase(int amount)
    {
        return coin >= amount;
    }
    public void Purchase(int amount)
    {
        coin -= amount;
        UpdateCoin();
    }
    public void Earn(int amount)
    {
        coin += amount;
        UpdateCoin();
    }
    public void UpdateCoin()
    {
        coinText.text = coin.ToString() + " gold";
    }
}
