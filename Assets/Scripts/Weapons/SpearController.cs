using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : UseableWeaponController
{

    public int projectileSpeed = 16;
    public GameObject projectile;

    public override void StartUseWeapon(Vector3 pos, Vector3 dir)
    {
        GameObject newProjectile = Instantiate(projectile, pos, Quaternion.LookRotation(dir));
        newProjectile.GetComponentInChildren<WeaponController>().Owner = Owner;

        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        rb.velocity = dir * projectileSpeed;

        Destroy(newProjectile, 10.0f);
    }

    public override void StopUseWeapon()
    {
        gameObject.SetActive(false);
    }
}
