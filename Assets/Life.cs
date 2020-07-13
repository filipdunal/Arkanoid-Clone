using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int life = 3;
    public Rigidbody2D ballRigidbody;
    public SpriteRenderer lifeIndicator;
    public Animator gameOverAnimator;
    public Animator levelCompletedAnimator;

    public static int points;

    public int ballsAmount = 1;

    Vector2 lifeIndicatorOriginalSize;
    private void Start()
    {
        lifeIndicatorOriginalSize = lifeIndicator.size;
        LifeIndicatorUpdate();
    }
    public void BallLoss(GameObject ball)
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
                FindObjectOfType<Ball>().GetComponent<Rigidbody2D>().simulated = false;
                ballsAmount++;
            }
            LifeIndicatorUpdate(true);
        }
        else
        {
            Destroy(ball);
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;

        Progress progress = FindObjectOfType<Progress>();
        progress.SaveData(true);
        progress.gameover = true;
        gameOverAnimator.Play("GameOver",-1,0f);
    }

    float oneLifeTileWidth = 0.53f;
    public void LifeIndicatorUpdate(bool blink=false)
    {
        lifeIndicatorFixedSize = new Vector2(oneLifeTileWidth * life, lifeIndicatorOriginalSize.y);
        lifeIndicator.size = lifeIndicatorFixedSize;
        if (blink)
        {
            StartCoroutine(LifeIndicatorBlink(lifeIndicatorFixedSize));
        }
    }

    Vector2 lifeIndicatorFixedSize;
    IEnumerator LifeIndicatorBlink(Vector2 originalSize)
    {
        for(int i=0;i<3;i++)
        {
            lifeIndicator.size = Vector2.zero;
            yield return new WaitForSeconds(0.15f);
            lifeIndicator.size = originalSize;
            yield return new WaitForSeconds(0.15f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Ball")
        {
            BallLoss(collision.collider.gameObject);
        }
    }
}
