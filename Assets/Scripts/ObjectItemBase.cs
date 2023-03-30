using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItemBase : MonoBehaviour,ITaker//класс для простых предметов
{
    public bool canMove = false;//можно перемещать
    public int CurrentCellPosition = 0;
    public Direction direction = Direction.North;
    public float Weight = 1.0f;
    public int Volume = 1; //объем объекта
    private Transform _itemTransform;
    
    public TItemName.AllItemName nameItem = TItemName.AllItemName.Пустотой;
    private TItemName.AllItemName ChildName = TItemName.AllItemName.Пустотой;//container
     
    public float speedPercent = 0.7f;//moveable
    void Awake()
    {
        _itemTransform = GetComponent<Transform>();
    }

    public virtual GlobalVariables.StatusAction ApplyTake(GlobalVariables.StatusAction sAction, string requiredObject, ObjectItemModuleTaker taker)
    {
        if (requiredObject == nameItem.ToString() + ChildName.ToString())
        {
            if (taker.loadCapacityMin <= Weight && taker.loadCapacityMax >= Weight)
            {
                sAction.ExseptionList.Add(GlobalVariables.StatusActionExseption.IsOk);
                sAction.OutTargetTransform = _itemTransform;
                return sAction;
            }
            else
            {
                sAction.ExseptionList.Add(GlobalVariables.StatusActionExseption.IsToolDoesNotFit);
                return sAction; 
            }
        }
        else
        {
            sAction.ExseptionList.Add(GlobalVariables.StatusActionExseption.IsNotFound);
            ObjectItemBase[] childs = GetComponentsInChildren<ObjectItemBase>();
            foreach (ObjectItemBase child in childs)
                if (child.TryGetComponent(out ITaker takerObject))
                {
                    sAction = takerObject.ApplyTake(sAction, requiredObject, taker);
                    if (sAction.ExseptionList[sAction.ExseptionList.Count-1] == GlobalVariables.StatusActionExseption.IsOk && sAction.OutTargetTransform != null)
                    {
                        // добавить проверку если детей теперь нет то ChildName=AllItemName.Пустотой
                        return sAction;
                    }
                }
            return sAction;
        }
    }
}
