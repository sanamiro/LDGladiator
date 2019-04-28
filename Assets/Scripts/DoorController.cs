using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private bool hasClosed = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasClosed)
            CloseDoor();
    }

    public void CloseDoor()
    {
        if (transform.position.y <= 2.40f)
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        else
            hasClosed = true;

    }
}
