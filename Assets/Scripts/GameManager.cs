using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TileManager tileManager;

    [SerializeField]
    UIManager uiManager;

    [SerializeField]
    Player player;

    [SerializeField]
    Cam cam;

    [SerializeField]
    SaveManager saveManager;

    [SerializeField]
    StagePanel stagePanel;

    [SerializeField]
    StageData currentStageData;

    [SerializeField]
    int gainCoin;

    [SerializeField]
    int gainStar;

    [SerializeField]
    bool isGamePlay;

    public void PlayerTriggerEnter(GameObject obj)
    {
        if (obj.name == Map.ObjType.Coin.ToString())
        {
            gainCoin++;
            Destroy(obj);
        }
        else if (obj.name == Map.ObjType.Star.ToString())
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

    void SaveStageData()
    {
        stagePanel.InitStageBtns(saveManager.StageDatas);
        stagePanel.Open();
    }

    void OnClickStageBtn(StageData data)
    {
        isGamePlay = true;
        gainCoin = 0;
        gainStar = 0;
        currentStageData = data;
    }

    private void Awake()
    {
        saveManager.loadEvent += SaveStageData;
        stagePanel.selectStageBtn += OnClickStageBtn;
    }

    private void Start()
    {
        //tileManager.Init();
        player.Init(this, tileManager);
    }

    private void LateUpdate()
    {
        cam.MoveTarget(player.transform.position);
    }

    private void OnDestroy()
    {
        saveManager.loadEvent -= SaveStageData;
        stagePanel.selectStageBtn -= OnClickStageBtn;
    }
}
