using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : UseableWeaponController
{
    public override void StartUseWeapon(Vector3 pos, Vector3 dir)
    {
        gameObject.SetActive(true);
        transform.position = pos;
    }

    public override void StopUseWeapon()
    {
        gameObject.SetActive(false);
    }
}
