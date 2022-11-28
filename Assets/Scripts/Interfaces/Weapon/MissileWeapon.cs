using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWeapon : MonoBehaviour, IWeapon
{
    float fireRate = 0.1f;
    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    float fireDelay = .5f;
    public float FireDelay
    {
        get => fireDelay;
        set => fireDelay = value;
    }

    float shotRange = 10f;
    public float ShotRange
    {
        get { return shotRange; }
        set { shotRange = value; }
    }

    float damage = 5f;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
}
