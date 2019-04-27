using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public CharacterController Owner;

    private void OnTriggerEnter(Collider col)
    {
        if (Owner is PlayerController)
        {
            EnemyController enemy = col.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.OnDamaged(this);
            }
        }

        if (Owner is EnemyController)
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                player.OnDamaged(this);
            }
        }

    }

    public float Damage
    {
        get => Owner.Damage;
    }
}
