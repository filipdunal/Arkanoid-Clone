using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float maxSpeed = 4f;
    Rigidbody2D rb;
    SpriteRenderer ballSprite;
    Transform paddle;

    [Tooltip("Minimum value of Y velocity. Value below it will be corrected to avoid endless loop of colliding ball between two colliders")]
    public float minimumYSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        paddle = GameObject.Find("Paddle").transform;
    }
    private void Update()
    {
        ballSprite.transform.rotation = Quaternion.identity;

        if (rb.simulated)
        {
            rb.velocity *= 10f;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else
        {
            MoveToPaddle();
            if(Input.GetKey(KeyCode.Space))
            {
                KickBall();
            }
        }

        if(Mathf.Abs(rb.velocity.y)<minimumYSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x,minimumYSpeed*Mathf.Sign(rb.velocity.y));
        }
        
    }

    public void MoveToPaddle()
    {
        transform.position = paddle.position + new Vector3(0f, 0.4f, 0f);
    }
    void KickBall()
    {
        rb.simulated = true;
        rb.AddForce(Vector3.up*10f);
    }
}
