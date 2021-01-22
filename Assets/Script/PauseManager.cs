using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
    }
}
