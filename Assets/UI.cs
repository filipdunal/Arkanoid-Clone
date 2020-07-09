using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text score;
    public Text highscore;

    float defaultCameraSize;
    public float width;
    private void Start()
    {
        defaultCameraSize = Camera.main.orthographicSize;
    }
    private void OnGUI()
    {
        score.text = Progress.points.ToString();
    }
    private void Update()
    {
        Camera.main.ResetAspect();
        if(Camera.main.aspect<1f)
        {
            Camera.main.orthographicSize = width * (float)Screen.height / (float)Screen.width * 0.5f;
        }
        else
        {
            Camera.main.orthographicSize = defaultCameraSize;
        }
    }

}
