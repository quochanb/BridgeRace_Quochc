using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;

    public override void SetUp()
    {
        base.SetUp();
        UpdateCoin(0);
    }

    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void SettingButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
        GameManager.Instance.OnSetting();
    }
}
