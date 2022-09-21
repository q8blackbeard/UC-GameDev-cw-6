using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;
    public Transform gchk;
    public bool isGround;
    public float rad;
    public float JumpForce;
    public LayerMask ground;
    bool facR = true;
    SpriteRenderer sprite;
    Animator anim;
    public string currAnim;
    const string IDLE_ANIM = "Idle";
    const string WALK_ANIM = "Walk";
    const string JUMP_ANIM = "jmp";

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
        PlayerMove();
        PlayerJump();
        flip();
    }
    void PlayerMove()
    {
        float xPos = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(xPos, rb.velocity.y);
        if (isGround && xPos == 0 && rb.velocity.y == 0)
        {

            PlayAnim(IDLE_ANIM);

        }

        else if (isGround && xPos != 0 && rb.velocity.y == 0)

        {

            PlayAnim(WALK_ANIM);

        }
    }
    void PlayerJump()
    {
        isGround = Physics2D.OverlapCircle(gchk.position,rad,ground);
        if(isGround && Input.GetKey(KeyCode.Space))
          
            {
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                PlayAnim(JUMP_ANIM);
            }

    }
    void flip()
    {
        if(Input.GetKey(KeyCode.D) && !facR)
        {
            sprite.flipX= false;
            facR = true;
        }else if(Input.GetKey(KeyCode.A) && facR)
        {
            sprite.flipX = true;
            facR = false;
        }
    }
    void PlayAnim(string toPlay)
    {
        if( currAnim == toPlay)
        {
            return;
        }
        currAnim = toPlay;
        anim.Play(toPlay);
    }
}
