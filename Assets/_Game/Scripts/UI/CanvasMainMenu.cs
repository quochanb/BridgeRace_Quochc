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
        Close(0);
        UIManager.Instance.OpenUI<CanvasJoystick>();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.OnGamePlay();
    }

    public void SettingButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
        GameManager.Instance.OnSetting();
    }

    public void ResumeButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasJoystick>();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.OnResume();
    }
}
