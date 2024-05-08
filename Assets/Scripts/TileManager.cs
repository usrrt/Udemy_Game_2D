using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 타일맵 데이터로 관리
[System.Serializable] // 직렬화시켜 인스펙터로 수정
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

    public void Init() // 게임매니저에서 호출
    {
        SetTileDatas();
        SpawnTileObjs();
    }

    public bool HasTileData(Vector3Int coord, string name)
    {
        return tileDatas.Exists(x => x.name == name && x.coord == coord); // 이름과 좌표의 존재여부 판단
    }

    public TileData GetTileData(string name)
    {
        return tileDatas.Find(x => x.name == name);
    }

    void SetTileDatas()
    {
        tileDatas = new();

        // 타일 이름과 위치정보 TileData 리스트에 추가
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

        // 자식 지우기
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0));
        }

        foreach(TileData tileData in tileDatas)
        {
            // 이름으로 구분
            GameObject prefab = prefabs.Find(x => x.name == tileData.name);
            if (prefab == null)
                continue;

            // 오브젝트와 중복되는 타일맵 지우기
            tilemap.SetTile(tileData.coord, null); // null -> 빈공간으로 교체됨
            GameObject tileObj = Instantiate(prefab, tileData.coord, Quaternion.identity, transform);
            tileObj.name = tileData.name; // 생성된 오브젝트 이름 뒤에 clone 표시 X
            tileObjs.Add(tileObj); 
        }
    }
}
