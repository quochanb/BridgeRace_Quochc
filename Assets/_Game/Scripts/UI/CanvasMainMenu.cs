using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        GameManager.Instance.ChangeGameState(GameState.GamePlay);
    }

    public void SettingButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Setting);
    }

    public void ResumeButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Resume);
    }
}
