using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public string WeaponSide;

    private void OnTriggerEnter(Collider col)
    {
        if (WeaponSide == "player")
        {
            EnemyController enemy = col.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.OnDamaged(this);
            }
        }

        if (WeaponSide == "enemy")
        {
            Debug.Log("testouille");
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                player.OnDamaged(this);
            }
        }

    }
}
