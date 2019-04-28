using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : WeaponController
{
    protected override void OnCollide(Collider com)
    {
        Destroy(gameObject);
    }

    protected override void OnDamage(CharacterController character)
    {
        base.OnDamage(character);
        Destroy(gameObject);
    }
}
