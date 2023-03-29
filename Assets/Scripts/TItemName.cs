using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TItemName : MonoBehaviour
{
    public enum AllItemName
    {   Пустотой, 
        УчастокЗемли, Булыжник, Камень, Вода, ЖелезнаяРуда, 
        Железо, Ведро, Ящик, Палетта
    }

    public List<string> ItemNameResources = new List<string>();
    public List<string> ItemNameContainers = new List<string>();
    public List<string> ItemNameRobots = new List<string>();//при создании робота добавлять его имя
    
 
    
    private void Awake()
    {
        ItemNameResources.Add(AllItemName.Булыжник.ToString());
        ItemNameResources.Add(AllItemName.Вода.ToString());
        ItemNameResources.Add(AllItemName.Железо.ToString());
        ItemNameResources.Add(AllItemName.Камень.ToString());
        ItemNameResources.Add(AllItemName.ЖелезнаяРуда.ToString());
        
        ItemNameContainers.Add(AllItemName.УчастокЗемли.ToString());
        ItemNameContainers.Add(AllItemName.Ведро.ToString());
        ItemNameContainers.Add(AllItemName.Палетта.ToString());
        ItemNameContainers.Add(AllItemName.Ящик.ToString());
    }
}
