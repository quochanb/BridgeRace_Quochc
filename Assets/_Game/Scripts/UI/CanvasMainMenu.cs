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
        GameManager.Instance.ChangeGameState(GameState.GamePlay);
        LevelManager.Instance.OnLoadLevel(0);
        LevelManager.Instance.OnLoadCharacter();
    }

    public void SettingButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Setting);
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }

    public void ResumeButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Resume);
    }
}
