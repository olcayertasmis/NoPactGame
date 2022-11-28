using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWeapon : MonoBehaviour, IWeapon
{
    float fireRate = .1f;
    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    float fireDelay = 1.5f;
    public float FireDelay
    {
        get => fireDelay;
        set => fireDelay = value;
    }

    float shotRange = 30f;
    public float ShotRange
    {
        get { return shotRange; }
        set { shotRange = value; }
    }

    float damage = 20f;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
}
