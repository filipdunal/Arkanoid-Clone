using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text score;
    public Text highscore;

    private void OnGUI()
    {
        score.text = Progress.points.ToString();
    }
    

}
