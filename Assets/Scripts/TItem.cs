using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TItem : MonoBehaviour, ITaker
{
    public enum AllItemName
    {   Пустотой,
        Булыжник, Камень, Вода, ЖелезнаяРуда, Железо,
        Ведро, Ящик, Палетта
    }

    public List<string> ItemNameResources = new List<string>();
    public List<string> ItemNameContainers = new List<string>();
    
    public AllItemName Name = AllItemName.Пустотой;
    public AllItemName ChildName = AllItemName.Пустотой;
    public float Weight = 1.0f;
    public int Volume = 1; //объем объекта
    private Transform _itemTransform;

    public TModuleTaker.StatusTaker ApplyTake(string requiredObject, TModuleTaker taker, Transform takerParent, Vector3 handPosition)
    {
        if (requiredObject == Name.ToString() + ChildName.ToString())
        {
            if (taker.loadCapacityMin <= Weight && taker.loadCapacityMax >= Weight)
            {
                _itemTransform.parent = takerParent;
                _itemTransform.transform.position = handPosition;
                
                return TModuleTaker.StatusTaker.isOk;
            }
            else
            {
                return TModuleTaker.StatusTaker.isToolDoesNotFit; 
            }
        }
        else
        {
            TModuleTaker.StatusTaker result = TModuleTaker.StatusTaker.isNotFound;
            
            TItem[] childs = GetComponentsInChildren<TItem>();
            foreach (TItem child in childs)
                if (child.TryGetComponent(out ITaker takerObject))
                {
                    result = takerObject.ApplyTake(requiredObject,taker,takerParent,handPosition);
                    if (result == TModuleTaker.StatusTaker.isOk)
                    {
                        // добавить проверку если детей теперь нет то ChildName=AllItemName.Пустотой
                        return TModuleTaker.StatusTaker.isOk;
                        break;//???
                    }
                }

            return result;
        }
        

    }

    private void Awake()
    {
        _itemTransform = GetComponent<Transform>();
        
        ItemNameResources.Add(AllItemName.Булыжник.ToString());
        ItemNameResources.Add(AllItemName.Вода.ToString());
        ItemNameResources.Add(AllItemName.Железо.ToString());
        ItemNameResources.Add(AllItemName.Камень.ToString());
        ItemNameResources.Add(AllItemName.ЖелезнаяРуда.ToString());
        
        ItemNameContainers.Add(AllItemName.Ведро.ToString());
        ItemNameContainers.Add(AllItemName.Палетта.ToString());
        ItemNameContainers.Add(AllItemName.Ящик.ToString());
    }
}
