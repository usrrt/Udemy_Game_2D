using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // event�� �ܺ� Ŭ�������� invoke�� �����ϴ� Ư���� delegate
    public event Action loadEvent;

    [SerializeField] SaveGameData saveGameData;

    public int Coin
    {
        get { return saveGameData.coin; }
        private set { saveGameData.coin = value; }
    }

    public List<StageData> StageDatas
    {
        get { return saveGameData.stageDatas; }
        set { saveGameData.stageDatas = value; }
    }

    // ���ø����̼��� �������� �����͸� ������ �� �ִ� ��ο� SaveData.json ���� �̸��� �߰��Ͽ� ��ü ���� ��θ� ��Ÿ��
    private string SavePath
    {
        get
        {
            // C:\Users\������̸�\AppData\LocalLow\ȸ���̸�\������Ʈ�̸� �� �����
            return $@"{Application.persistentDataPath}\SaveData.jason";
        }
    }

    [ContextMenu("ClearSaveFile")]
    void ClearSaveFile()
    {
        File.Delete(SavePath);
    }

    public void SaveData()
    {
        // saveClass ��ü�� JSON�������� ����ȭ �Ͽ� ���ڿ����·� ����
        string jsonData = JsonUtility.ToJson(saveGameData);
        // SavePath��ο� jsonData������ ��
        File.WriteAllText(SavePath, jsonData);
    }

    public void LoadData()
    {
        if(File.Exists(SavePath))
        {
            string jsonData = File.ReadAllText(SavePath);
            saveGameData = JsonUtility.FromJson<SaveGameData>(jsonData);
        }
        else
        {
            Coin = 0;
            StageDatas = new();

            for (int i = 0; i < 10000; i++)
            {
                // ������ ����ϸ� File.Exists�� ���� ���� ���翩�θ� Ȯ���ϴ°� ����, �������� ����ϸ� try-catch�� ����� ������ ������� ������� ���ܸ� ó���Ҽ��ִ�. �ٸ� �ش� �ڵ�� �ܼ� ���� ���翩�� �ľ��̹Ƿ� File.Exists�� ����ϴ°� �� ȿ�����̴�.

                string levelPath = $@"{Application.dataPath}\Resources\Levels\Levle{i}.json";

                if (!File.Exists(levelPath))
                    break;

                StageData stageData = new StageData()
                {
                    level = i,
                    star = 0,
                    isLock = i > 1
                };

                StageDatas.Add(stageData);

                //try
                //{
                //    string levelPath = $@"{Application.dataPath}\Resources\Levels\Levle{i}.json";

                //    StageData stageData = new StageData()
                //    {
                //        level = i,
                //        star = 0,
                //        isLock = i > 1
                //    };

                //    StageDatas.Add(stageData);
                //}
                //catch (FileNotFoundException)
                //{
                //    break;
                //}

            }
        }
    }

    private void Start()
    {
        LoadData();
        loadEvent?.Invoke();
    }
}
