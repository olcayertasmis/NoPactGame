using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerBike : MonoBehaviour, IUnits
{
    private Unit _unit;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float health;
    [SerializeField] private GameObject weaponGo;
    [SerializeField] private IWeaponType weaponType;


    // float movementSpeed = 6f;
    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    // float health = 900f;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    private void Start()
    {
        _unit.myUnit = this;

        switch (weaponType)
        {
            case IWeaponType.CANNON:
                var weapon = this.weaponGo.AddComponent<CannonWeapon>();
                _unit.myWeapon = weapon;
                break;
            case IWeaponType.MISSILE:
                var weapon1 = this.weaponGo.AddComponent<MissileWeapon>();
                _unit.myWeapon = weapon1;
                break;
        }
    }
}
