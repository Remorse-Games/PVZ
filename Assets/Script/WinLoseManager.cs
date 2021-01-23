using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour
{
    public int hp = 10, wave = 0, nextSceneIdx;
    private int maxWave;
    public Text hpText, waveText;
    public static WinLoseManager instance;
    public GameObject endScreen;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UpdateWave();
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
                //gameover
                Time.timeScale = 0f;
            }
        }
    }
    public void SetMaxWave(int wave)
    {
        maxWave = wave;
    }
    public void NextWave()
    {
        wave++;
        if (maxWave < wave)
        {
            Win();
        }
        else
        {
            UpdateWave();
        }
    }
    private void UpdateHP()
    {
        hpText.text = hp.ToString();
    }
    private void UpdateWave()
    {
        waveText.text = "X" + wave.ToString();
    }
    private void Win()
    {
        endScreen.SetActive(true);
    }
    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
