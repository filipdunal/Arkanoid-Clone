using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedFactor = 1f;
    Rigidbody2D rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal")*speedFactor, 0f, 0f);
        rigidbody.velocity=movementVector;
    }
    void SetWidth(float width)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, width);
        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        collider.size = new Vector2(collider.size.x+0.45f, collider.size.y);

    }
}
