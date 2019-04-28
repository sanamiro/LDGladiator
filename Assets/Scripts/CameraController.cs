using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(0,20,0);
        float newX = PlayerPos.position.x;
        float newZ = PlayerPos.position.z;

        Debug.Log(newZ);

        if (Mathf.Pow((newX / 15), 2) + Mathf.Pow((newZ / 10), 2) > 1)         //Si on sort de l'ellipse voulue
        {
            if (!(newX <= 0.01f && -0.01f <= newX))
            {
                newPos = new Vector3(PlayerPos.position.x, 20, PlayerPos.position.z + 30);

                if (newX < 0 && newX != 0)                                         //Si on est à gauche du centre (y positifs)
                {
                    float x = newX;
                    float y = (newZ / newX) * x;
                    while (Mathf.Pow((x / 15), 2) + Mathf.Pow((y / 10), 2) > 1)
                    {
                        x += 0.01f;
                        y = (newZ / newX) * x;
                    }
                    newPos.x = x;
                    newPos.z = (newZ / newX) * x + 30;
                }

                else if (newX > 0 && newX != 0)                                    //Si on est à droite du centre (x positifs)
                {
                    float x = newX;
                    float y = (newZ / newX) * x;
                    while (Mathf.Pow((x / 15), 2) + Mathf.Pow((y / 10), 2) > 1)
                    {
                        x -= 0.01f;
                        y = (newZ / newX) * x;
                    }

                    newPos.x = x;
                    newPos.z = (newZ / newX) * x + 30;
                }

            }
            else
            {
                if (newZ <= -10)
                    newZ = -10;
                else if (10 <= newZ)
                    newZ = 10;

                newPos.x = newX;
                newPos.z = newZ + 30;
            }
        }
        else
            newPos = new Vector3(PlayerPos.position.x, 20, PlayerPos.position.z + 30);

        transform.position = newPos;
    }
}
