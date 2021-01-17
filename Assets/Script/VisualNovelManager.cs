using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VisualNovelManager : MonoBehaviour
{
    [SerializeField]
    private List<VideoClip> cutscenes = null;
    private VideoPlayer videoPlayer;
    private int length, current;
    [SerializeField]
    private int sceneIndex;
    // Start is called before the first frame update
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        length = cutscenes.Count;
        current = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (current >= length-1)
            {
                videoPlayer.Stop();
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                current++;
                videoPlayer.Stop();
                videoPlayer.clip = cutscenes[current];
                videoPlayer.Play();
            }
        }
    }
}
