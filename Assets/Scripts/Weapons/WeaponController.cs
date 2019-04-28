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
                OnDamage(enemy);
            }
            else if (col.GetComponent<PlayerController>() == null)
            {
                OnCollide(col);
            }
        }

        if (Owner is EnemyController)
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                OnDamage(player);
            }
            else if (col.GetComponent<EnemyController>() == null)
            {
                OnCollide(col);
            }
        }

    }

    protected virtual void OnDamage(CharacterController character)
    {
        character.OnDamaged(this);
    }

    protected virtual void OnCollide(Collider com)
    { }

    public float Damage
    {
        get => Owner.Damage;
    }
}
