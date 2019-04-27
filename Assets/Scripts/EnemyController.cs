using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void OnDamaged(WeaponController weapon)
    {
        this.gameObject.SetActive(false);
    }
}
