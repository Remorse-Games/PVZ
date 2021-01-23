using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private int coin = 0, score = 0;
    public int startingCoin = 200;
    public Text coinText, scoreText, endScoreText;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        coin = startingCoin;
        UpdateCoin();
        score = 0;
        UpdateScore();
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
        score += 500;
        UpdateScore();
    }
    public void UpdateCoin()
    {
        coinText.text = coin.ToString();
    }
    public void UpdateScore()
    {
        endScoreText.text = score.ToString();
        scoreText.text = score.ToString();
    }
}
