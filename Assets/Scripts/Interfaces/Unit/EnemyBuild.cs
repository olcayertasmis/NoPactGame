using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuild : MonoBehaviour, IUnits
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float health;


    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public float Health
    {
        get { return health; }
        set { health = value; }
    }
}
