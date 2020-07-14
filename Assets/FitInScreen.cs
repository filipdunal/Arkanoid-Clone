using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitInScreen : MonoBehaviour
{
    float defaultCameraSize;
    public float width;
    public float defaultAspect = 1f;
    private void Start()
    {
        defaultCameraSize = Camera.main.orthographicSize;
    }
    private void Update()
    {
        Camera.main.ResetAspect();
        if (Camera.main.aspect < defaultAspect)
        {
            Camera.main.orthographicSize = width * (float)Screen.height / (float)Screen.width * 0.5f;
        }
        else
        {
            Camera.main.orthographicSize = defaultCameraSize;
        }
    }
}
