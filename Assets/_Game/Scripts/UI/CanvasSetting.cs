using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    [SerializeField] GameObject[] buttons;

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
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    public void ContinueButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }

    public void CloseButton()
    {
        Close(0);
    }
}
