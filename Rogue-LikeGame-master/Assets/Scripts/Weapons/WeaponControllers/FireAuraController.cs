using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAuraController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedFireAura = Instantiate(weaponData.Prefab);
        spawnedFireAura.transform.position = transform.position;
        spawnedFireAura.transform.parent = transform;

    }
}
