using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public bool isCompletedForestNormalMode;
    public bool isCompletedDungeonNormalMode;

    public Data(bool isCompletedForestNormalMode, bool isCompletedDungeonNormalMode)
    {
        this.isCompletedForestNormalMode = isCompletedForestNormalMode;
        this.isCompletedDungeonNormalMode = isCompletedDungeonNormalMode;
    }

    public Data()
    {
        isCompletedForestNormalMode = false;
        isCompletedDungeonNormalMode = false;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
   
}
