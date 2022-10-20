using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpHeight;
    public Transform groundCheck;  
    Animator anim;
    bool isGrounded;
    [SerializeField] float curHp;
    [SerializeField] float maxHp = 3;
    bool isHit = false;
    //public Main main;
    public bool CanTp = true;
    bool isClimbing = false;  //анимация лестницы


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    { 


        CheckGround();
        if (Input.GetAxis("Horizontal") == 0 && (isGrounded) && !isClimbing)
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            Flip();
            if(isGrounded && !isClimbing)
               anim.SetInteger("State", 2);
           
        }
       
        FixedUpdate();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpHeight,ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
        {
            anim.SetInteger("State", 3);
        }
    }

    public void RecountHp(float deltaHp)
    {
        curHp = curHp + deltaHp;
        if (deltaHp < 0)
        { 
            
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit());
        }
        else if (curHp > maxHp)
        {
            curHp = curHp + deltaHp;
            curHp = maxHp;
        }
        if (curHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1f);
        }
    }

    IEnumerator OnHit()
    {
        if (isHit)
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.04f, GetComponent<SpriteRenderer>().color.b - 0.04f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.04f, GetComponent<SpriteRenderer>().color.b + 0.04f);

        if (GetComponent<SpriteRenderer>().color.g == 1f)
        {
            StopCoroutine(OnHit());
        }

        if (GetComponent<SpriteRenderer>().color.g - 0.02f <= 0) 
            isHit = false;

        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    }

    public void Lose()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            if (collision.gameObject.GetComponent<Door>().isOpen && CanTp)
            {
                collision.gameObject.GetComponent<Door>().Teleport(gameObject);
                CanTp = false;
                StartCoroutine(TpWait());
            }
        }
    }

    IEnumerator TpWait()
    {
        yield return new WaitForSeconds(1f);
        CanTp = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ladder")
        {
            isClimbing = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            if(Input.GetAxis("Vertical") == 0)
            {
                anim.SetInteger("State", 4);
            }
            else
            {
                anim.SetInteger("State", 5);
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * 0.5f * Time.deltaTime);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isClimbing = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
