using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    public enum UserDirection {Vertical, Horizontal};
    [SerializeField] UserDirection enemyDirection;
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    Animator animator;
    Vector2 initialPos;
    Vector2 newPos;
    string directionPos;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        if (enemyDirection.ToString() == "Horizontal")
            transform.localRotation = Quaternion.Euler(0, 0, 90);
    }
    void FixedUpdate()
    {
        if (enemyDirection.ToString() == "Horizontal")
        {
            rb.velocity = new Vector2 (moveSpeed, 0f);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }            
        else
            rb.velocity = new Vector2 (0f, moveSpeed);
        AnimSprite(CalPosition());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.tag == "Walls") { moveSpeed = -moveSpeed; }
        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Walls"))) {
            //Debug.Log("walls");
            moveSpeed = -moveSpeed;
            }
        
        FlipSprite();
    }

    void FlipSprite()
    {
        // transform.localScale = new Vector2 (Mathf.Sign(rb.velocity.x), 1f);
        transform.localScale = new Vector2 (-(Mathf.Sign(rb.velocity.x)), 1f);
    }

    string CalPosition()
    {
        //Debug.Log(initialPos.x + " " + newPos.x);
        if (initialPos.x < newPos.x && initialPos.y == newPos.y)
        {
            directionPos = "Right";
        }
        else if (initialPos.x == newPos.x && initialPos.y < newPos.y)
        {
            directionPos = "Up";
        }
        else if (initialPos.x > newPos.x && initialPos.y == newPos.y)
        {
            directionPos = "Left";
        }
        else if (initialPos.x == newPos.x && initialPos.y > newPos.y)
        {
            directionPos = "Down";
        }
        return directionPos;
    }

    void AnimSprite(string switchCase)
    {
        switch (switchCase)
            {
                case "Right":
                    animator.SetTrigger("isRight");
                    break;
                case "Left":
                    animator.SetTrigger("isLeft");
                    break;
                case "Up":
                    animator.SetTrigger("isBehind");
                    break;
                case "Down":
                    animator.SetTrigger("isFront");
                    break;
            }
    }
}
