using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Map
{
    public enum ObjType
    {
        None,
        Wall,
        Start,
        End,
        Coin,
        Star,
    }

    public ObjType objType;
}

[Serializable]
public class SaveGameData
{
    public int coin;
    public List<StageData> stageDatas;
}

[Serializable]
public class StageData
{
    public int level;
    public int star;
    public bool isLock;
}