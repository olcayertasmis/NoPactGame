using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    float ShotRange { get; set; }
    float Damage { get; set; }
    float FireRate { get; set; }
    float FireDelay { get; set; }
}


