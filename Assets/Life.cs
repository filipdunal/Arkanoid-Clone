using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int life = 3;
    public Rigidbody2D ballRigidbody;
    public SpriteRenderer lifeIndicator;

    public static int points;

    int ballsAmount = 1;
    private void Start()
    {
        LifeIndicatorUpdate();
    }
    public void BallLoss()
    {
        ballsAmount--;
        if(ballsAmount<1)
        {
            life--;
            if (life < 1)
            {
                GameOver();
            }
            else
            {
                ballRigidbody.simulated = false;
            }
        }
        LifeIndicatorUpdate(true);
        
    }

    void GameOver()
    {
        Time.timeScale = 0f;
    }

    float oneLifeTileWidth = 0.53f;
    void LifeIndicatorUpdate(bool blink=false)
    {
        lifeIndicator.size = new Vector2(oneLifeTileWidth * life, lifeIndicator.size.y);
        if (blink)
        {
            StartCoroutine(LifeIndicatorBlink(lifeIndicator.size));
        }
        
    }

    IEnumerator LifeIndicatorBlink(Vector2 originalSize)
    {
        for(int i=0;i<3;i++)
        {
            lifeIndicator.size = Vector2.zero;
            yield return new WaitForSeconds(0.2f);
            lifeIndicator.size = originalSize;
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.layer);
        if(collision.collider.tag=="Ball")
        {
            BallLoss();
        }
    }
}
