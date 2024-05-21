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

    StageData stageData;

    private void Awake()
    {
        btnImg = GetComponent<Image>();
    }

    public void Init(StageData stageData)
    {
        this.stageData = stageData;
        stageTxt.text = stageData.level.ToString();
        btnImg.color = stageData.isLock ? Color.gray : Color.yellow;

        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].gameObject.SetActive(!stageData.isLock);
            starIcons[i].color = i < stageData.star ? Color.yellow : Color.clear;
        }
    }

    // stageData�� �������� UI ��Ҹ� �����ϴ� ����
    /*
    1. ������ ���� �� ����: stageData�� ���� �ܺο��� ����� �� �ֽ��ϴ�. ���� ���, Ư�� �ܰ谡 ��� �����ǰų� ���� ������ ����� ��, ����� �����͸� �ݿ��ϱ� ���� UI�� �����ؾ� �մϴ�. Renew �޼��带 ȣ���Ͽ� ����� �����͸� �ݿ��� �� �ֽ��ϴ�.

    2. ���뼺: UI ��Ҹ� �ʱ�ȭ�ϴ� ������ Init �޼��忡 ���ԵǾ� �����Ƿ�, ������ ������ �����Ͽ� UI�� ������ �� �ֽ��ϴ�. �̷��� �ϸ� �ߺ� �ڵ带 ���� �� �ֽ��ϴ�.

    3. ����������: UI �ʱ�ȭ �� ���� ������ �� ���� ���ߵǾ� �־� ���������� �����մϴ�. ���� ���, UI ���� ������ �����ؾ� �� ��� Init �޼��常 �����ϸ� �˴ϴ�. Renew �޼���� Init �޼��带 ȣ���ϱ� ������ �߰����� ������ �ʿ� �����ϴ�.
     */
    public void Renew()
    {
        Init(stageData);
    }
}
