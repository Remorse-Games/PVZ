using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject currentActive;
    public GameObject mainMenuCanvas;
    public Image ensPanel;
    public Sprite[] ensDetails;
    private void Start()
    {
        ChangeCanvas(mainMenuCanvas);
    }
    public void ChangeCanvas(GameObject canvas)
    {
        if (currentActive != null) 
            currentActive.SetActive(false);
        currentActive = canvas;
        currentActive.SetActive(true);
    }
    public void SetEnsDetails(int idx)
    {
        ensPanel.sprite = ensDetails[idx];
    }
    public void OpenScene(int level)
    {
        SceneManager.LoadScene(level);
    }
}
