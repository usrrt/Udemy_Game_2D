using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    TileManager tileManager;

    public bool isMoveable;
    
    // ���ӸŴ����� Ÿ�ϸŴ����� �˱� ���� �ܺο��� �Է¹ޱ� => ������ ��� ���
    public void Init(GameManager gameManager, TileManager tileManager)
    {
        this.gameManager = gameManager;
        this.tileManager = tileManager;

        TileData startTileData = tileManager.GetTileData("Start");
        transform.position = startTileData.coord;
    }

    void InputKeyDown(Vector3Int dir)
    {
        Vector3Int currentCoord = Vector3Int.RoundToInt(transform.position); // ������ġ �ݿø�(�Ҽ�������)
        Vector3Int targetCoord = currentCoord;

        for (int i = 0; i < 1000; i++)
        {
            // ������ġ���� dir���� 1000���� ���� ��ĭ�� �˻�
            Vector3Int checkCoord = currentCoord + dir * i;
            if (tileManager.HasTileData(checkCoord, "Wall")) // ���� ������ �����
                break;
            if (tileManager.HasTileData(checkCoord, "End"))
            {
                targetCoord = checkCoord;
                break;
            }

            targetCoord = checkCoord;
             
        }
        
        transform.position = targetCoord;
    }

    private void Update()
    {
        if(isMoveable)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow)) InputKeyDown(Vector3Int.up);
            else if(Input.GetKeyDown(KeyCode.DownArrow)) InputKeyDown(Vector3Int.down);
            else if(Input.GetKeyDown(KeyCode.RightArrow)) InputKeyDown(Vector3Int.right);
            else if(Input.GetKeyDown(KeyCode.LeftArrow)) InputKeyDown(Vector3Int.left);
        }
    }
}