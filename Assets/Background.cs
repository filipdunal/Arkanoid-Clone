using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public void ChangeColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
        spriteRenderer.color = randomColor;
    }
    private void Start()
    {
        ChangeColor();
    }
}
