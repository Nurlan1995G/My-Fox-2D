using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumPatrol : MonoBehaviour
{
    public float speed = 2f;
    public bool moveLeft = true;
    public Transform groundDetect;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
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
}
