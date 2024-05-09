using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : UICanvas
{
    [SerializeField] Button[] buttons;

    public void SetState(UICanvas canvas)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        if(canvas is CanvasMainMenu)
        {
            buttons[2].gameObject.SetActive(true);
        }

        if(canvas is CanvasGamePlay)
        {
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
        }
    }

    public void MainMenuButton()
    {
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
        //LevelManager.Instance.OnReset();
    }

    public void ContinueButton()
    {
        GameManager.Instance.ChangeGameState(GameState.GamePlay);
    }

    public void CloseButton()
    {
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }
}
