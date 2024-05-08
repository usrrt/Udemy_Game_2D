using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileManager tileManager;
    public Player player;

    private void Start()
    {
        tileManager.Init();
        player.Init(this, tileManager);
    }
}
