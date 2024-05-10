using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] Button[] buttons;
    public void SetState(int levelNumber)
    {
        if (levelNumber == 0)
        {
            buttons[1].gameObject.SetActive(false);
        }
        else
        {
            buttons[1].gameObject.SetActive(true);
        }
    }

    public void PlayButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasJoystick>();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.ChangeGameState(GameState.GamePlay);
    }

    public void SettingButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
        GameManager.Instance.ChangeGameState(GameState.Setting);
    }

    public void ResumeButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasJoystick>();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.ChangeGameState(GameState.Resume);
    }
}
