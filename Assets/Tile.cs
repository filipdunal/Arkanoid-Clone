using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tileLife = 1;
    public int points;
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
        Destroy(gameObject);
    }
}
