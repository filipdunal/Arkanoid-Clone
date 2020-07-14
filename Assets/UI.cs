using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text score;
    public Text highscore;
    public Text level;

    public Transform bigPaddleIndicator;
    public Transform smallPaddleIndicator;
    public Transform stickyPaddleIndicator;

    private void OnGUI()
    {
        level.text = Progress.level.ToString();
        score.text = Progress.points.ToString();
        highscore.text = Progress.highscore.ToString();
    }
}
