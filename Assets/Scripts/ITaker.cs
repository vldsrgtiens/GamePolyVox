using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITaker
{
    public TModuleTaker.StatusTaker ApplyTake(string requiredObject, TModuleTaker taker, Transform takerParent, Vector3 handPosition);
}
