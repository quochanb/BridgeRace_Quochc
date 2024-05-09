using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI scoreText;

    public override void SetUp()
    {
        base.SetUp();
        UpdateScore(0);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SettingButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Setting);
    }
}
