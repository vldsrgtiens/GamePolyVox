using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectList : MonoBehaviour
{
    public static Dictionary<int, ObjectItemBase> dObjectList = new Dictionary<int, ObjectItemBase>();
    public static int indexItem = 0;


    public static int AddItem(Vector3 posItem, float rotItem, ObjectItemBase objPrefab)
    {
        
        
        dObjectList.Add(indexItem,Instantiate<ObjectItemBase>(objPrefab));
        dObjectList[indexItem].transform.localPosition = posItem;
        dObjectList[indexItem].transform.rotation = Quaternion.Euler(0f,rotItem,0f);

        indexItem++;
        //Debug.Log("ÓbjectList - "+dObjectList.Count);
        return indexItem-1;
    }
}
