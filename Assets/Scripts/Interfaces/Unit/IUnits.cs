using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnits
{
    float MovementSpeed { get; set; }
    float Health { get; set; }
}

public enum IWeaponType
{
    CANNON,
    MISSILE
}
