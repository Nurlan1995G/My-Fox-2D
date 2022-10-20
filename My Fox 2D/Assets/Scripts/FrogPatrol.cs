using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPatrol : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float speed = 2;
    public bool moveLeft = true;
    public Transform groundDetect;
    public float jumpHeight;
    bool isGrounded;
    public Transform groundCheck;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        RaycastHit2D grounInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 1f);   //луч направленный вниз на 1 клетку

        if (grounInfo.collider == false)
        {
            if (moveLeft == true)
            {
                
                transform.eulerAngles = new Vector3(0, 180, 0);
                moveLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveLeft = true;
            }
        }
    }

    IEnumerator FrogId()
    {
        yield return new WaitForSeconds(3f);
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
        {
            anim.SetInteger("State", 2);
        }
    }


}
