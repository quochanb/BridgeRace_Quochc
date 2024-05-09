using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] TextMeshProUGUI scoreText;

    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void MainMenuButton()
    {
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }

    public void NextButton()
    {
        GameManager.Instance.ChangeGameState(GameState.NextLevel);
    }
}
