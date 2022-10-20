using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stake : MonoBehaviour
{
    public float speed = 3f;
    bool isWait = false;  // время ожидания спратынных кольев
    bool isHidden = false;  //колышки спрятаны
    public float waitTime = 4f;  
    public Transform point;   //точка перемещения наших колышек

    private void Start()
    {
        point.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
    }

    private void Update()
    {
        if (isWait == false)
            transform.position = Vector3.MoveTowards(transform.position, point.position, speed * Time.deltaTime);

        if (transform.position == point.position)
        {
            if (isHidden)
            {
                point.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
                isHidden = false;
            }
            else
            {
                point.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
                isHidden = true;
            }
            isWait = true;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        isWait = false;
    }
}
