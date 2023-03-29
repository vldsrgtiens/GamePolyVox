using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TModuleTaker : MonoBehaviour
{
    //public enum StatusTaker { isOk, isNotFound, isToolDoesNotFit }
    public float loadCapacityMin = 0.1f;
    public float loadCapacityMax = 2.0f;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public GlobalVariables.StatusAction TakeObject(TItemName.AllItemName objectName)
    {
        GlobalVariables.StatusAction result = new GlobalVariables.StatusAction(new List<GlobalVariables.StatusActionExseption>(),null);
        var taker = HexGrid.cells[5].transform.GetComponent<ITaker>();
        if (taker != null)
        {
            result = taker.ApplyTake(result,TItemName.AllItemName.Железо.ToString(),this);
            Debug.Log("Попробовал забрать железо, результат - "+result.ToString());
        }
        else
        {
            result.ExseptionList.Add(GlobalVariables.StatusActionExseption.IsNotFound);
        }
        return result;
    }
}
