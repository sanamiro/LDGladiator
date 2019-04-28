using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseableWeaponController : WeaponController
{
    public abstract void StartUseWeapon(Vector3 pos, Vector3 dir);

    public virtual void StopUseWeapon()
    { }
}
