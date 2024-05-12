using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TileManager tileManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] Player player;
    [SerializeField] Cam cam;
    [SerializeField] SaveManager saveManager;

    [SerializeField] int gainCoin;
    [SerializeField] int gainStar;
    [SerializeField] bool isGamePlay;

    public void PlayerTriggerEnter(GameObject obj)
    {
        if (obj.name == Map.ObjType.Coin.ToString())
        {
            gainCoin++;
            Destroy(obj);
        }
        else if(obj.name == Map.ObjType.Star.ToString())
        {
            gainStar++;
            Destroy(obj);
        }
        else if (obj.name == Map.ObjType.End.ToString())
        {
            Goal();
        }
    }

    void Goal()
    {
        uiManager.ShowResultPanel(gainCoin, gainStar);
    }

    public void ClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        tileManager.Init();
        player.Init(this, tileManager);
    }


    private void LateUpdate()
    {
        cam.MoveTarget(player.transform.position);
    }
}
