using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectList : MonoBehaviour
{
    public static Dictionary<int, ObjectItem> dObjectList = new Dictionary<int, ObjectItem>();
    public static int indexItem = 0;


    public static int AddItem(Vector3 posItem, float rotItem, ObjectItem objPrefab)
    {
        
        
        dObjectList.Add(indexItem,Instantiate<ObjectItem>(objPrefab));
        dObjectList[indexItem].transform.localPosition = posItem;
        dObjectList[indexItem].transform.rotation = Quaternion.Euler(0f,rotItem,0f);

        indexItem++;
        //Debug.Log("ÓbjectList - "+dObjectList.Count);
        return indexItem-1;
    }
}
