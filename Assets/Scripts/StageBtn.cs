using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    [SerializeField]
    Text stageTxt;

    [SerializeField]
    Image[] starIcons;

    Image btnImg;

    StageData StageData;

    private void Awake()
    {
        btnImg = GetComponent<Image>();
    }

    public void Init(StageData stageData)
    {
        stageTxt.text = stageData.level.ToString();
        btnImg.color = stageData.isLock ? Color.gray : Color.yellow;

        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].gameObject.SetActive(!stageData.isLock);
            starIcons[i].color = i < stageData.star ? Color.yellow : Color.clear;
        }
    }
}
