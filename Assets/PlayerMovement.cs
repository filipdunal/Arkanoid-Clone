using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedFactor = 1f;
    public float speedOfPaddleTransition = 1f;
    public UI ui;
    //public float smallPaddleWidthMultipler = 0.5f;
    //public float bigPaddleWidthMultipler = 2f;
    float normalPadleWidth;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Transform collide;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalPadleWidth = spriteRenderer.size.x;
        collide = transform.GetChild(0);
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

    public float timeOfPaddleSizeChange=10f;
    IEnumerator TimedPowerUp()
    {
        yield return new WaitForSecondsRealtime(timeOfPaddleSizeChange);
        SetPaddleSize(1f);
    }
    
    public void SetPaddleSize(float width)
    {
        //spriteRenderer.size = new Vector2(normalPadleWidth*width, spriteRenderer.size.y);
        //collide.localScale = new Vector3(normalPadleWidth*width,1f,1f);

        Vector2 desireSpriteSize = new Vector2(normalPadleWidth * width, spriteRenderer.size.y);
        Vector3 desireColliderSize= new Vector3(normalPadleWidth * width, 1f, 1f);

        StopAllCoroutines();
        StartCoroutine(TimedPowerUp());
        StartCoroutine(AnimatePaddle(desireSpriteSize, desireColliderSize));

        if (width==1f)
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
        
    }

    IEnumerator AnimatePaddle(Vector2 spriteSize, Vector3 colliderSize)
    {
        while((spriteRenderer.size-spriteSize).magnitude>0.01f)
        {
            spriteRenderer.size = Vector2.Lerp(spriteRenderer.size, spriteSize, Time.deltaTime * speedOfPaddleTransition);
            collide.localScale = Vector3.Lerp(collide.localScale, colliderSize, Time.deltaTime * speedOfPaddleTransition);
            yield return null;
        }
    }
}
