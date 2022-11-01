using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bx;
    public Text CoinsText;
    public Text ScoreText;
    public bool ReadyJump;
    public int ForceJump;
    float score;
    float coin;
    public bool isAlive = true;
    public float Highscore;
    private float cooldown;
    private float NextBoostTime = 0;

    void Start()
    {
        bx = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        score = 0;
        coin = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 0);
            bx.size = new Vector2(1, 0.5f);
            bx.offset = new Vector2(0, -0.25f);
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(2, 2, 0);
            bx.size = new Vector2(1, 1);
            bx.offset = new Vector2(0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (ReadyJump)
            {
                rb.AddForce(new Vector2(0, ForceJump));
                ReadyJump = false;
            }
        }
        if(isAlive)
        {
            score += Time.deltaTime * 4;
            ScoreText.text = "Счет: " + score.ToString("F");
            CoinsText.text = "Монеток: " + coin.ToString();
            if (score >= Highscore)
            {
                Highscore = score;
                PlayerPrefs.SetFloat("Рекорд", Highscore);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Time.time > NextBoostTime)
            {
                if (coin >= 2)
                {
                    rb.AddForce(Vector2.up * ForceJump * 1.5f);
                    coin = coin - 2;
                    NextBoostTime = Time.time + cooldown;
                }
            }
        }
    }

    public void JumpPlayer()
    {
        ReadyJump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            ReadyJump = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            isAlive = false;
            Time.timeScale = 0;
        }
        if (collision.gameObject.tag == "Coin")
        {
            coin++;
        }
    }
}
