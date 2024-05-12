using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI levelName;

    public void SetBestScore(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void SetLeveName(int levelNumber)
    {
        levelName.text = "LEVEL " + (levelNumber + 1).ToString();
    }

    public void MainMenuButton()
    {
        UIManager.Instance.CloseAll();
        GameManager.Instance.OnMainMenu();
    }

    public void NextButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasJoystick>();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.OnNextLevel();
    }
}
