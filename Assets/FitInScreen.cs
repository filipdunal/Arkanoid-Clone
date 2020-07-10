using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitInScreen : MonoBehaviour
{
    float defaultCameraSize;
    public float width;
    private void Start()
    {
        defaultCameraSize = Camera.main.orthographicSize;
    }
    private void Update()
    {
        Camera.main.ResetAspect();
        if (Camera.main.aspect < 1f)
        {
            Camera.main.orthographicSize = width * (float)Screen.height / (float)Screen.width * 0.5f;
        }
        else
        {
            Camera.main.orthographicSize = defaultCameraSize;
        }
    }
}
