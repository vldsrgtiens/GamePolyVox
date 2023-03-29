using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITaker
{
    public GlobalVariables.StatusAction ApplyTake(GlobalVariables.StatusAction sAction,string requiredObject, TModuleTaker taker);
}


