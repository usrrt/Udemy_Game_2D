using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Ÿ�ϸ� �����ͷ� ����
[System.Serializable] // ����ȭ���� �ν����ͷ� ����
public class TileData
{
    public string name;
    public Vector3Int coord;
}

public class TileManager : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileData> tileDatas;
    public List<GameObject> tileObjs;
    public List<GameObject> prefabs;

    public void Init() // ���ӸŴ������� ȣ��
    {
        SetTileDatas();
        SpawnTileObjs();
    }

    public bool HasTileData(Vector3Int coord, string name)
    {
        return tileDatas.Exists(x => x.name == name && x.coord == coord); // �̸��� ��ǥ�� ���翩�� �Ǵ�
    }

    public TileData GetTileData(string name)
    {
        return tileDatas.Find(x => x.name == name);
    }

    void SetTileDatas()
    {
        tileDatas = new();

        // Ÿ�� �̸��� ��ġ���� TileData ����Ʈ�� �߰�
        foreach(Vector3Int coord in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(coord))
                continue;

            TileData tileData = new();
            tileData.name = tilemap.GetTile(coord).name;
            tileData.coord = coord;
            tileDatas.Add(tileData);
        }
    }

    void SpawnTileObjs()
    {
        tileObjs = new();   

        // �ڽ� �����
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0));
        }

        foreach(TileData tileData in tileDatas)
        {
            // �̸����� ����
            GameObject prefab = prefabs.Find(x => x.name == tileData.name);
            if (prefab == null)
                continue;

            // ������Ʈ�� �ߺ��Ǵ� Ÿ�ϸ� �����
            tilemap.SetTile(tileData.coord, null); // null -> ��������� ��ü��
            GameObject tileObj = Instantiate(prefab, tileData.coord, Quaternion.identity, transform);
            tileObj.name = tileData.name; // ������ ������Ʈ �̸� �ڿ� clone ǥ�� X
            tileObjs.Add(tileObj); 
        }
    }
}
