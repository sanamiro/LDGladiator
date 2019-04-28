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

        /*if (Mathf.Pow((newX / 19), 2) + Mathf.Pow((newZ / 15), 2) > 1)         //Si on sort de l'ellipse voulue
        {
            if (newX <= 0 && newZ != 0)                                         //Si on est à gauche du centre (y positifs)
            {
                float x = newX;
                float y = (newX / newZ) * x;
                while (Mathf.Pow((x / 19), 2) + Mathf.Pow((y / 15), 2) > 1)
                {
                    x += 0.1f;
                    y = (newX / newZ) * x;
                }

                newPos.x = x;
                newPos.z = (newX / newZ) * x + 30;
            }

            else if (newX >= 0 && newZ != 0)                                    //Si on est à droite du centre (x positifs)
            {
                float x = newX;
                float y = (newX / newZ) * x;
                while (Mathf.Pow((x / 19), 2) + Mathf.Pow((y / 15), 2) > 1)
                {
                    x -= 0.1f;
                    y = (newX / newZ) * x;
                }

                newPos.x = x;
                newPos.z = (newX / newZ) * x + 30;
            }

            else if (newX <= 0 && newZ == 0)                                    //Si on est sur l'axe des abcisses (x+ ou x-)
                newPos = new Vector3(22, 0, 30);
            else if (newX >= 0 && newZ == 0)
                newPos = new Vector3(-22, 0, 30);
        }
        else
            newPos = new Vector3(PlayerPos.position.x, 20, PlayerPos.position.z + 30);*/

        if (newX <= -15)
            newX = -15;
        if (newX >= 15)
            newX = 15;

        if (newZ <= -15)
            newZ = -15;
        if (newZ >= 10)
            newZ = 10;

        newPos.x = newX;
        newPos.z = newZ + 30;

        transform.position = newPos;
    }
}
