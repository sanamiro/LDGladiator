using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    public float Health;

    public abstract float Damage { get; }

    protected virtual float Armor { get => 1.0f; }

    public virtual void OnDamaged(WeaponController weapon)
    {
        Health -= weapon.Damage / Armor;
        if (Health <= 0) OnDeath();
    }

    public abstract void OnDeath();
}