using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    //public List<GameObject> tileObjs;
    //public List<GameObject> prefabs;
    public Tilemap tilemap;

    [SerializeField]
    List<TileData> tileDatas;

    [SerializeField]
    int saveLoadLevel;

    LevelData loadLevelData;

    #region OLD
    //public void Init() // ���ӸŴ������� ȣ��
    //{
    //    SetTileDatas();
    //    SpawnTileObjs();
    //}

    //public bool HasTileData(Vector3Int coord, string name)
    //{
    //    return tileDatas.Exists(x => x.name == name && x.coord == coord); // �̸��� ��ǥ�� ���翩�� �Ǵ�
    //}

    //public UnityEngine.Tilemaps.TileData GetTileData(string name)
    //{
    //    return tileDatas.Find(x => x.name == name);
    //}

    //void SetTileDatas()
    //{
    //    tileDatas = new();

    //    // Ÿ�� �̸��� ��ġ���� TileData ����Ʈ�� �߰�
    //    foreach (Vector3Int coord in tilemap.cellBounds.allPositionsWithin)
    //    {
    //        if (!tilemap.HasTile(coord))
    //            continue;

    //        UnityEngine.Tilemaps.TileData tileData = new();
    //        tileData.name = tilemap.GetTile(coord).name;
    //        tileData.coord = coord;
    //        tileDatas.Add(tileData);
    //    }
    //}

    //void SpawnTileObjs()
    //{
    //    tileObjs = new();

    //    // �ڽ� �����
    //    while (transform.childCount > 0)
    //    {
    //        DestroyImmediate(transform.GetChild(0));
    //    }

    //    foreach (UnityEngine.Tilemaps.TileData tileData in tileDatas)
    //    {
    //        // �̸����� ����
    //        GameObject prefab = prefabs.Find(x => x.name == tileData.name);
    //        if (prefab == null)
    //            continue;

    //        // ������Ʈ�� �ߺ��Ǵ� Ÿ�ϸ� �����
    //        tilemap.SetTile(tileData.coord, null); // null -> ��������� ��ü��
    //        GameObject tileObj = Instantiate(
    //            prefab,
    //            tileData.coord,
    //            Quaternion.identity,
    //            transform
    //        );
    //        tileObj.name = tileData.name; // ������ ������Ʈ �̸� �ڿ� clone ǥ�� X
    //        tileObjs.Add(tileObj);
    //    }
    //}

    #endregion

    public bool LoadLevel(int level, out Vector2Int startPos)
    {
        saveLoadLevel = level;
        bool isLoad = LoadLevelData();
        startPos = Vector2Int.zero;
        if (isLoad)
        {
            startPos = loadLevelData.maps.Find(x => x.objType == Map.ObjType.Start).coord;
        }
        return isLoad;
    }

    public bool IsContainTile(Map.ObjType type, Vector2Int checkCoord)
    {
        return loadLevelData.maps.Exists(x => x.objType == type && x.coord == checkCoord);
    }

    #region Ÿ�ϸ� ������

    [ContextMenu("SaveLevelData")]
    void SaveLevelData()
    {
        LevelData levelData = BuildLevelData();
        string jsonData = JsonUtility.ToJson(levelData);
        string path = $@"{Application.dataPath}\Resources\Levels\Level{saveLoadLevel}.json";
        File.WriteAllText(path, jsonData);

        Debug.Log($"Level{saveLoadLevel}.json ����");
    }

    [ContextMenu("LoadLevelData")]
    bool LoadLevelData()
    {
        string path = $@"{Application.dataPath}\Resources\Levels\Level{saveLoadLevel}.json";
        if (!File.Exists(path))
            return false;

        string jsonData = File.ReadAllText(path);
        loadLevelData = JsonUtility.FromJson<LevelData>(jsonData);
        Debug.Log($"Level{saveLoadLevel}.json �ҷ���");
        //Debug.Log(loadLevelData);

        ClearView();
        CreateView(loadLevelData);

        return true;
    }

    [ContextMenu("ClearView")]
    void ClearView()
    {
        tilemap.ClearAllTiles();
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    void CreateView(LevelData data)
    {
        foreach (Map map in data.maps)
        {
            TileData tileData = tileDatas.Find(x => x.type == map.objType);
            Vector3Int coord = new Vector3Int(map.coord.x, map.coord.y, 0);
            if (tileData.isObject)
            {
                GameObject obj = Instantiate(tileData.pf, coord, Quaternion.identity, transform);
                obj.name = map.objType.ToString();
            }
            else
            {
                tilemap.SetTile(coord, tileData.tileBase);
            }
        }
    }

    LevelData BuildLevelData()
    {
        List<Map> maps = new();
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos))
                continue;

            Map map = new Map
            {
                objType = Enum.Parse<Map.ObjType>(tilemap.GetTile(pos).name),
                coord = new Vector2Int(pos.x, pos.y),
            };
            maps.Add(map);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            Map map = new Map
            {
                objType = Enum.Parse<Map.ObjType>(obj.name),
                coord = new Vector2Int(
                    Mathf.RoundToInt(obj.transform.position.x),
                    Mathf.RoundToInt(obj.transform.position.y)
                )
            };
            if (maps.Contains(map))
                continue;

            maps.Add(map);
        }

        return new LevelData() { maps = maps };
    }

    #endregion
}
