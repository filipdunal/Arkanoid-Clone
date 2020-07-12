using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPowerUps : MonoBehaviour
{
    public int stickyPaddleHits=5;
    int stickyPaddleHitsLeft;
    public UI ui;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(stickyPaddleHitsLeft!=0 && collision.collider.tag=="Ball")
        {
            collision.collider.GetComponent<Rigidbody2D>().simulated = false;
            stickyPaddleHitsLeft--;
            if(stickyPaddleHitsLeft==0)
            {
                ui.stickyPaddleIndicator.gameObject.SetActive(false);
            }
            return;
        }
        PowerUpBall powerUpBall = collision.collider.GetComponent<PowerUpBall>();
        if (powerUpBall == null) return;

        switch (powerUpBall.powerUp)
        {
            case PowerUp.BigPaddle:
                transform.parent.GetComponent<PlayerMovement>().SetPaddleSize(2f);
                break;

            case PowerUp.SmallPaddle:
                transform.parent.GetComponent<PlayerMovement>().SetPaddleSize(0.5f);
                break;

            case PowerUp.OneMoreBall:
                Ball ball=FindObjectOfType<Ball>();
                if (ball!=null)
                {
                    Transform newBall= Instantiate(ball.gameObject).transform;
                    newBall.name = "Ball";
                    FindObjectOfType<Life>().ballsAmount++;
                }
                break;
            case PowerUp.StickyPaddle:
                stickyPaddleHitsLeft = stickyPaddleHits;
                ui.stickyPaddleIndicator.gameObject.SetActive(true);
                break;

            case PowerUp.AddLife:
                Life life = FindObjectOfType<Life>();
                life.life++;
                life.LifeIndicatorUpdate();
                break;

            case PowerUp.None:
                break;
        }

        Destroy(powerUpBall.gameObject);
    }
}
