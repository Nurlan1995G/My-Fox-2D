using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public Transform door;


    public void Unlock()
    {
        isOpen = true;
    }

    public void Teleport(GameObject fox)
    {
        fox.transform.position = new Vector3(door.position.x, door.position.y, fox.transform.position.z);
    }
}
