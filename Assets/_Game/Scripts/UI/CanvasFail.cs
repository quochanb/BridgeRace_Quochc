using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;

    public void SetBestScore(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void MainMenuButton()
    {
        UIManager.Instance.CloseAll();
        GameManager.Instance.OnMainMenu();
    }

    public void PlayAgainButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasJoystick>();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.OnResume();
    }
}
