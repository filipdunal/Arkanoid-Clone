using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPowerUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PowerUpBall powerUpBall = collision.collider.GetComponent<PowerUpBall>();
        if(powerUpBall!=null)
        {
            Destroy(powerUpBall.gameObject);
        }
    }
}
