using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tileLife = 1;
    public int points;
    public PowerUp powerUp=PowerUp.None;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Ball")
        {
            tileLife--;
            if(tileLife<1)
            {
                Destory();
            }
        }
    }

    void Destory()
    {
        Progress.points += points;
        InstantiatePowerUp();

        Destroy(gameObject);
    }

    void InstantiatePowerUp()
    {
        if(powerUp!=PowerUp.None)
        {
            Instantiate(GetComponentInParent<LevelGenerator>().powerUps[(int)powerUp-1], transform.position, Quaternion.identity);
        }
        /*
        switch(powerUp)
        {
            case PowerUp.BigPaddle:
                Instantiate(GetComponentInParent<LevelGenerator>().powerUps[0], transform.position,Quaternion.identity);
                break;

            case PowerUp.SmallPaddle:
                Instantiate(GetComponentInParent<LevelGenerator>().powerUps[1], transform.position, Quaternion.identity);
                break;

            case PowerUp.None:
                break;
        }
        */
    }
}
