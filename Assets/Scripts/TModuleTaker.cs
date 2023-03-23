using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TModuleTaker : MonoBehaviour
{
    public enum StatusTaker { isOk, isNotFound, isToolDoesNotFit }
    public float loadCapacityMin = 0.1f;
    public float loadCapacityMax = 2.0f;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public StatusTaker TakeObject(TItem.AllItemName objectName)
    {
        StatusTaker result = StatusTaker.isNotFound;
        var taker = HexGrid.cells[5].transform.GetComponent<ITaker>();
        if (taker != null)
        {
            result = taker.ApplyTake(TItem.AllItemName.Железо.ToString(),this,_transform,Vector3.zero);
            Debug.Log("Попробовал забрать железо, результат - "+result.ToString());
        }
        return result;
    }
}
