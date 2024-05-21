using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
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
    public Vector2Int coord;
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

[Serializable]
public class TileData
{
    public Map.ObjType type;
    public bool isObject;
    public TileBase tileBase;
    public GameObject pf;
}

[Serializable]
public struct LevelData
{
    public List<Map> maps;
}
