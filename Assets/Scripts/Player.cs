using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    TileManager tileManager;

    public bool isMoveable;
    public float speed;
    
    // 게임매니저와 타일매니저를 알기 위해 외부에서 입력받기 => 인젝션 기법 사용
    public void Init(GameManager gameManager, TileManager tileManager)
    {
        this.gameManager = gameManager;
        this.tileManager = tileManager;

        TileData startTileData = tileManager.GetTileData("Start");
        transform.position = startTileData.coord;
    }

    void InputKeyDown(Vector3Int dir)
    {
        Vector3Int currentCoord = Vector3Int.RoundToInt(transform.position); // 현재위치 반올림(소수점방지)
        Vector3Int targetCoord = currentCoord;

        for (int i = 0; i < 1000; i++)
        {
            // 현재위치에서 dir방향 1000블록 까지 한칸씩 검사
            Vector3Int checkCoord = currentCoord + dir * i;
            if (tileManager.HasTileData(checkCoord, "Wall")) // 벽이 나오면 멈춘다
                break;
            if (tileManager.HasTileData(checkCoord, "End"))
            {
                targetCoord = checkCoord;
                break;
            }

            targetCoord = checkCoord;
        }
        
        StartCoroutine(MoveCor(targetCoord));
        Rotate(dir);
    }

    IEnumerator MoveCor(Vector3Int targetCoord)
    {
        isMoveable = false; // 움직이는 동안 입력방지
        while(transform.position != targetCoord)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, targetCoord, Time.deltaTime * speed);
        }
        isMoveable = true;
    }

    void Rotate(Vector3Int dir)
    {
        float angleZ = 0;
        if (dir == Vector3Int.up)
            angleZ = 180;
        else if (dir == Vector3Int.down)
            angleZ = 0;
        else if (dir == Vector3Int.right)
            angleZ = 90;
        else if (dir == Vector3Int.left)
            angleZ = 270;

        transform.rotation = Quaternion.Euler(0, 0, angleZ);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.PlayerTriggerEnter(collision.gameObject);
    }
}
