using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    [Range(0,100)]
    public float speed;
    public float JumpForce;
    public bool ReadyJump;
    public bool isEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(new Vector2(-1, 0) * speed);
        if (Mathf.Abs(rb.velocity.x) > speed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speed, rb.velocity.y);
        if(!isEnemy)
        {
            return;
        }
        if (ReadyJump)
        {
            rb.AddForce(new Vector2(0, JumpForce));
            ReadyJump = false;
        }
        if (gameObject.transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ReadyJump = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
        }
    }
}
