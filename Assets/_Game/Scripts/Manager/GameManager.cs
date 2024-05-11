using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    MainMenu = 0,
    GamePlay = 1,
    Setting = 2,
    Victory = 3,
    Fail = 4,
}

public class GameManager : Singleton<GameManager>
{
    public GameState currentState;
    private int levelNumber;
    private int coin = 0;

    private void Awake()
    {
        //xu tai tho
        int maxScreenHeight = 1920;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    private void Start()
    {
        levelNumber = PlayerPrefs.GetInt("game_level", 0);
        OnMainMenu();
    }

    //change state
    public void ChangeGameState(GameState newState)
    {
        this.currentState = newState;
    }

    //goi khi can xu ly main menu
    public void OnMainMenu()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>().SetState(levelNumber);
        LevelManager.Instance.OnReset();
        ChangeGameState(GameState.MainMenu);
    }

    //goi khi can xu ly game play
    public void OnGamePlay()
    {
        levelNumber = 0;
        OnSetup();
    }

    //goi khi can xu ly setting
    public void OnSetting()
    {
        ChangeGameState(GameState.Setting);
    }

    //goi khi can xu ly victory
    public void OnVictory()
    {
        ChangeGameState(GameState.Victory);
        StartCoroutine(DelayTimeVictory(1));
    }

    //goi khi can xu ly fail
    public void OnFail()
    {
        int currentNumberLevel = levelNumber;
        PlayerPrefs.SetInt("game_level", currentNumberLevel);
        ChangeGameState(GameState.Fail);
        StartCoroutine(DelayTimeFail(1));
    }

    //goi khi can xu ly next level
    public void OnNextLevel()
    {
        levelNumber++;
        PlayerPrefs.SetInt("game_level", levelNumber);
        UIManager.Instance.GetUI<CanvasGamePlay>().UpdateCoin(coin);
        OnSetup();
    }

    //goi khi can xu ly choi lai
    public void OnResume()
    {
        levelNumber = PlayerPrefs.GetInt("game_level");
        OnSetup();
    }

    //goi khi can xu ly tiep tuc game
    public void OnContinue()
    {
        ChangeGameState(GameState.GamePlay);
    }

    //
    public void OnUpdateCoin()
    {
        UIManager.Instance.GetUI<CanvasGamePlay>().UpdateCoin(coin += 10);
    }

    //setup level
    private void OnSetup()
    {
        LevelManager.Instance.OnReset();
        LevelManager.Instance.OnLoadLevel(levelNumber);
        LevelManager.Instance.OnLoadCharacter();
        ChangeGameState(GameState.GamePlay);
    }

    //delay time fail
    IEnumerator DelayTimeFail(float time)
    {
        yield return new WaitForSeconds(time);
        UIManager.Instance.OpenUI<CanvasFail>();
        UIManager.Instance.GetUI<CanvasFail>().SetBestScore(coin);
    }

    //delay time win
    IEnumerator DelayTimeVictory(float time)
    {
        yield return new WaitForSeconds(time);
        UIManager.Instance.OpenUI<CanvasVictory>();
        UIManager.Instance.GetUI<CanvasVictory>().SetBestScore(coin);
    }
}
