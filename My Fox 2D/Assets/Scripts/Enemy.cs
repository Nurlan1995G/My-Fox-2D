using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool isHit = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fox")
        {
            collision.gameObject.GetComponent<Fox>().RecountHp(-1f);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 4f, ForceMode2D.Impulse);
        }
    }
}
