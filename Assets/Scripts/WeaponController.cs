using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        EnemyController enemy = col.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.OnDamaged(this);
        }
    }
}
