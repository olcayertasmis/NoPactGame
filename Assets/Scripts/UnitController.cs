using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private List<Unit> _selectedUnitList;

    [SerializeField] private LayerMask whatIsUnitLayer, whatIsGroundLayer, whatIsEnemyLayer, whatIsYourGroundAndAttackableLayer;

    private void Awake()
    {
        _selectedUnitList = new List<Unit>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) SelectUnit(); // Mouse sol click basildiginda
        if (Input.GetMouseButtonDown(1)) MoveUnit(); // Mouse sag click basildiginda
    }

    // FONKSIYONLAR
    private void SelectUnit() // Unit secme
    {
        RaycastHit hit = CheckRay(whatIsUnitLayer);

        if (hit.collider == null)
        {
            return;
        }

        var getUnit = hit.collider.GetComponentInParent<Unit>();

        if (getUnit != null)
        {
            foreach (Unit unit in _selectedUnitList)
            {
                if (getUnit == unit)
                {
                    unit.SetUnselectedUnit();
                    _selectedUnitList.Remove(unit);
                    return;
                }
            }

            getUnit.SetSelectedUnit();
            _selectedUnitList.Add(getUnit);
        }
    }

    private void MoveUnit() // Unit hareketi
    {

        RaycastHit hit = CheckRay(whatIsYourGroundAndAttackableLayer);

        var place = hit.point;
        var targetEnemy = hit.collider.gameObject;
        var target = hit.transform.GetComponent<EnemyBuild>();

        if (hit.collider == null)
        {
            return;
        }

        if (targetEnemy.tag == "Attackable")
        {
            foreach (var unit in _selectedUnitList)
            {
                unit.SetTargetPosition(place, false, true);
                unit.SetTarget(target);
            }
            return;
        }

        List<Vector3> targetPositionList = GetPositionListAround(place, new float[] { 4f, 8f, 16f }, new int[] { 5, 10, 20 });

        int targetPositionListIndex = 0;

        if (hit.collider != null)
        {
            foreach (var unit in _selectedUnitList)
            {
                unit.SetTargetPosition(targetPositionList[targetPositionListIndex], true, false);
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
            }
        }
    }

    private RaycastHit CheckRay(LayerMask whatIsLayer) // Mouse hit belirleme
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity, whatIsLayer);

        return hit;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
}
