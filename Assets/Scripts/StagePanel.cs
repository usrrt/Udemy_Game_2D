using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePanel : MonoBehaviour
{
    public event Action<StageData> selectStageBtn;

    [SerializeField]
    GameObject panel;

    [SerializeField]
    GameObject stageBtnPf;

    [SerializeField]
    Transform stageBtnParent;

    [SerializeField]
    List<StageBtn> stageBtns;

    public void Open()
    {
        panel.SetActive(true);
        foreach (StageBtn btn in stageBtns)
        {
            btn.Renew();
        }
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void SelectStageBtn(StageData stageData)
    {
        if (stageData.isLock)
            return;

        selectStageBtn?.Invoke(stageData);
    }

    public void InitStageBtns(List<StageData> stageDatas)
    {
        stageBtns = new();
        foreach (StageData data in stageDatas)
        {
            StageBtn stageBtn = Instantiate(stageBtnPf, stageBtnParent).GetComponent<StageBtn>();
            stageBtn.Init(data);
            stageBtn.GetComponent<Button>().onClick.AddListener(() => SelectStageBtn(data));
            stageBtns.Add(stageBtn);
        }
    }
}
