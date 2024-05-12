using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // event는 외부 클래스에서 invoke를 방지하는 특수한 delegate
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

    // 애플리케이션의 지속적인 데이터를 저장할 수 있는 경로에 SaveData.json 파일 이름을 추가하여 전체 파일 경로를 나타냄
    private string SavePath
    {
        get
        {
            // C:\Users\사용자이름\AppData\LocalLow\회사이름\프로젝트이름 에 저장됨
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
        // saveClass 객체를 JSON형식으로 직렬화 하여 문자열형태로 저장
        string jsonData = JsonUtility.ToJson(saveGameData);
        // SavePath경로에 jsonData파일을 씀
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
                // 성능을 고려하면 File.Exists를 통해 파일 존재여부를 확인하는게 좋고, 안전성을 고려하면 try-catch를 사용해 파일이 없을경우 명시적인 예외를 처리할수있다. 다만 해당 코드는 단순 파일 존재여부 파악이므로 File.Exists를 사용하는게 더 효율적이다.

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
