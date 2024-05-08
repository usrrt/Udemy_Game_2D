using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileManager tileManager;
    public Player player;
    public Cam cam;

    public int gainCoin;
    public int gainStar;

    private void Start()
    {
        tileManager.Init();
        player.Init(this, tileManager);
    }

    private void LateUpdate()
    {
        cam.MoveTarget(player.transform.position);
    }

    public void PlayerTriggerEnter(GameObject obj)
    {
        if (obj.name == "Coin")
        {
            gainCoin++;
            Destroy(obj);
        }
        else if(obj.name == "Star")
        {
            gainStar++;
            Destroy(obj);
        }
        else if (obj.name == "End")
        {
            Goal();
        }
    }

    void Goal()
    {
        print("Success");
    }
}
