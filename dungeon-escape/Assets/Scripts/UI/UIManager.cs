using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is NULL.");
            }
            return _instance;
        }
    }

    public Text playerGemCountText;
    public Image selectionImg;
    public Text gemCountText;
    public Image[] lifeUnits;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gems)
    {
        playerGemCountText.text = gems + "G";
    }

    public void UpdateShopSelection(float yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLives(int livesRemaining)
    {
        lifeUnits[livesRemaining].enabled = false;
    }
}
