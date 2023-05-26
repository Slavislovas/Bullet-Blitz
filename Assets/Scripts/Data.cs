using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public bool isCompletedForestNormalMode;

    public Data(bool isCompletedForestNormalMode)
    {
        this.isCompletedForestNormalMode = isCompletedForestNormalMode;
    }

    public Data()
    {
        isCompletedForestNormalMode = false;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
   
}
