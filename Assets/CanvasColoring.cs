using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasColoring : MonoBehaviour
{
    public Image animatedImage;
    public Image[] imageToAnimate;
    public Text[] textToAnimate;

    private void Update()
    {
        foreach(Image im in imageToAnimate)
        {
            im.color = animatedImage.color;
        }

        foreach(Text text in textToAnimate)
        {
            text.color = animatedImage.color;
        }
    }
}
