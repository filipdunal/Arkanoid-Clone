using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedFactor = 1f;
    public UI ui;
    //public float smallPaddleWidthMultipler = 0.5f;
    //public float bigPaddleWidthMultipler = 2f;
    float normalPadleWidth;

    Rigidbody2D rb;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        normalPadleWidth = spriteRenderer.size.x;

        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if(rb.simulated)
        {
            Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal") * speedFactor, 0f, 0f);
            rb.velocity = movementVector;
        }  
    }

    float timeOfPaddleSizeChange=5f;
    IEnumerator TimedPowerUp()
    {
        yield return new WaitForSecondsRealtime(timeOfPaddleSizeChange);
        SetPaddleSize(1f);
    }
    
    public void SetPaddleSize(float width)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.size = new Vector2(normalPadleWidth*width, spriteRenderer.size.y);

        //CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        //collider.size = new Vector2(collider.size.x+0.45f, collider.size.y);

        Transform collider = transform.GetChild(0);
        collider.localScale = new Vector3(normalPadleWidth*width,1f,1f);

        StopAllCoroutines();

        if(width==1f)
        {
            ui.bigPaddleIndicator.gameObject.SetActive(false);
            ui.smallPaddleIndicator.gameObject.SetActive(false);
        }
        else if(width>1f)
        {
            ui.bigPaddleIndicator.gameObject.SetActive(true);
            ui.smallPaddleIndicator.gameObject.SetActive(false);
        }
        else
        {
            ui.bigPaddleIndicator.gameObject.SetActive(false);
            ui.smallPaddleIndicator.gameObject.SetActive(true);
        }
        StartCoroutine(TimedPowerUp());
    }

    
}
