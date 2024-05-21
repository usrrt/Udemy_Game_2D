using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject resultPanel;
    public List<Image> stars;
    public Text coinTxt;

    public void ShowResultPanel(int gainCoin, int gainStar)
    {
        resultPanel.SetActive(true);
        for (int i = 0; i < stars.Count; i++)
        {
            if (gainStar > i)
            {
                stars[i].color = Color.yellow;
            }
            else
            {
                stars[i].color = Color.black;
            }
        }
        coinTxt.text = $"얻은 코인 : {gainCoin}";
    }
}
