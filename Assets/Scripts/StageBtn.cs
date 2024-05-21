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

    // stageData를 기준으로 UI 요소를 갱신하는 역할
    /*
    1. 데이터 변경 후 갱신: stageData의 값이 외부에서 변경될 수 있습니다. 예를 들어, 특정 단계가 잠금 해제되거나 별의 개수가 변경될 때, 변경된 데이터를 반영하기 위해 UI를 갱신해야 합니다. Renew 메서드를 호출하여 변경된 데이터를 반영할 수 있습니다.

    2. 재사용성: UI 요소를 초기화하는 로직이 Init 메서드에 포함되어 있으므로, 동일한 로직을 재사용하여 UI를 갱신할 수 있습니다. 이렇게 하면 중복 코드를 피할 수 있습니다.

    3. 유지보수성: UI 초기화 및 갱신 로직이 한 곳에 집중되어 있어 유지보수가 용이합니다. 예를 들어, UI 갱신 로직을 변경해야 할 경우 Init 메서드만 수정하면 됩니다. Renew 메서드는 Init 메서드를 호출하기 때문에 추가적인 수정이 필요 없습니다.
     */
    public void Renew()
    {
        Init(stageData);
    }
}
