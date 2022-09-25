using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class player : MonoBehaviour
{


    Rigidbody2D rb;
    public float speed;
    public Transform groundCHK;
    public bool isGrounded;
    public float rad;
    public float jumpForce;
    bool facingRight = true;
    public LayerMask Ground;
    SpriteRenderer sprite;
    Animator anim;
    public string currentAnim;
    const string IDLE_ANIM = "idle";
    const string RUN_ANIM = "run";
    const string JUMP_ANIM = "jump";
    public GameObject bullet;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playermove();
        playerJump();
        Flip();

    }

     void Update()
    {
        shoot(); 
    }

    void playermove()
    {
        float xPos = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2 (xPos, rb.velocity.y);
        Flip();

        if(isGrounded && xPos == 0 &&rb.velocity.y == 0)
        {
            playAnim(IDLE_ANIM);
        }else if(isGrounded && xPos != 0 && rb.velocity.y == 0)
        {
            playAnim(RUN_ANIM);
        }
    }

    void playerJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCHK.position, rad, Ground);


        if(isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            playAnim(JUMP_ANIM);
        }
        
    }
    void Flip()
    {
        if (Input.GetKey(KeyCode.D) && !facingRight)
        {
            sprite.flipX = false;
            facingRight = true;
        }
        else if (Input.GetKey(KeyCode.A) && facingRight)
        {
            sprite.flipX = true;
            facingRight = false;
        }
    }

    void playAnim(string toPlay)
    {
        if(currentAnim == toPlay)
        {
            return;
        }
        currentAnim = toPlay;
        anim.Play(toPlay);
    }

    void shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
            if (facingRight)
            {
                bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0f);
            }
            else
            {
                bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0f);
            }Destroy(bulletClone, 1.5f);
        }
    }
}
