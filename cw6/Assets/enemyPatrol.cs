using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyPatrol : MonoBehaviour
{
    Rigidbody2D rb;
    public float eSpeed;
    public float rad;
    public Transform groundCheck;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
    void Patrol()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, rad, ground);
        rb.velocity = new Vector2(eSpeed, 0);
        if (!isGrounded)
        {
            eSpeed *= -1;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
      }
     void OnTriggerEnter2D(Collider2D collision)
        {
        
        {
            Destroy(gameObject);
            SceneManager.LoadScene(1);
        }
           
}
         void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(0);
            }
        }
    
}
