using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public IWeapon myWeapon;
    public IUnits myUnit;
    private EnemyBuild _enemy;

    private Renderer _renderer, _targetRenderer;

    private Vector3 _targetPosition;

    private bool _isSelected = false; // Olası kontrol durumu sağlamak için kullanılmıyor
    private bool _isMoving = false;
    private bool _isAttacking = false;

    private void Awake()
    {
        _renderer = this.gameObject.GetComponentInChildren<Renderer>();
    }

    private void Start()
    {
        SetUnselectedUnit();
    }

    private void Update()
    {
        if (_isMoving && !_isAttacking) Move(true);

        if (_isAttacking && !_isMoving) Attack();
        else if (_isMoving && _isAttacking) Move(false);
    }

    // FONKSIYONLAR
    public void SetSelectedUnit() // Secim yapildiginda
    {
        _renderer.material.SetColor("_Color", Color.green);
        _isSelected = true;
    }

    public void SetUnselectedUnit() // Secim iptal oldugunda
    {
        _renderer.material.SetColor("_Color", Color.white);
        _isSelected = false;
    }

    private void Move(bool JustMove) // Düz hareket ve shot range göre hareket
    {
        var stoppingDistance = 0f;

        if (JustMove) stoppingDistance = 0.05f;
        else stoppingDistance = myWeapon.ShotRange;

        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _targetPosition, myUnit.MovementSpeed * Time.deltaTime);
            transform.LookAt(_targetPosition);
        }
        else
        {
            _isMoving = false;
        }
    }

    private void Attack() // Saldırı 
    {
        if (!CheckShotRange())
        {
            // print("Move and Attack");
            _isMoving = true;  //Move

        }
        else if (CheckShotRange() && _enemy.Health > 0)
        {
            //Attack
            // print("Attack");
            ToDamage(_enemy);
        }
    }

    public void SetTarget(EnemyBuild target) // Saldırılan hedefi ayarlama
    {
        _enemy = target;
        _targetRenderer = _enemy.gameObject.GetComponent<Renderer>();
    }


    public void ToDamage(EnemyBuild target) // İstenen özelliklere göre hasar verme
    {
        if (target.Health > 0)
        {
            // print(myWeapon.FireRate);
            if (Time.time > myWeapon.FireRate || Time.time <1)
            {
                // print(target.Health);
                target.Health = target.Health - myWeapon.Damage;
                myWeapon.FireRate = Time.time + myWeapon.FireDelay;

                if (target.Health == 0) _targetRenderer.material.SetColor("_Color", Color.black);
            }
        }
    }

    private bool CheckShotRange() // Shot Range kontrolü
    {
        if (Vector3.Distance(transform.position, _targetPosition) > myWeapon.ShotRange)
        {
            return false;
        }

        transform.LookAt(_targetPosition);

        return true;
    }

    public void SetTargetPosition(Vector3 targetPosition, bool isMoving, bool isAttacking) // Target Pozisyonu ayarlama
    {
        this._isMoving = isMoving;
        this._isAttacking = isAttacking;

        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z); // Y'de sabit kalmasi icin
        this._targetPosition = targetPosition;
    }
}
