using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu = 0,
    GamePlay = 1,
    Setting = 2,
    Victory = 3,
    Fail = 4,
    NextLevel = 5,
    Resume =6
}

public class GameManager : Singleton<GameManager>
{
    public GameState currentState;
    private int levelNumber;

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
        ChangeGameState(GameState.MainMenu);
    }

    //change state
    public void ChangeGameState(GameState newState)
    {
        this.currentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                OnMainMenu();
                break;
            case GameState.GamePlay:
                OnGamePlay();
                break;
            case GameState.Setting:
                OnSetting();
                break;
            case GameState.Victory:
                OnVictory();
                break;
            case GameState.Fail:
                OnFail();
                break;
            case GameState.NextLevel:
                OnNextLevel();
                break;
            case GameState.Resume:
                OnResume();
                break;
            default:
                break;
        }
    }

    //state main menu
    public void OnMainMenu()
    {
        levelNumber = PlayerPrefs.GetInt("game_level", 0);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        LevelManager.Instance.OnLoadLevel(levelNumber);
        LevelManager.Instance.OnLoadCharacter();
        Time.timeScale = 0;
    }

    //state play game
    public void OnGamePlay()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        UIManager.Instance.OpenUI<CanvasJoystick>();
        Time.timeScale = 1;
    }

    //state setting
    public void OnSetting()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasSetting>();
        Time.timeScale = 0;
    }

    //state victory
    public void OnVictory()
    {
        StartCoroutine(DelayTimeVictory(2));
    }

    //state fail
    public void OnFail()
    {
        StartCoroutine(DelayTimeFail(1));
    }

    //state next
    private void OnNextLevel()
    {
        levelNumber++;
        PlayerPrefs.SetInt("game_level", levelNumber);
        OnSetup();
        Time.timeScale = 1;
    }

    //state resume
    private void OnResume()
    {
        levelNumber = PlayerPrefs.GetInt("game_level");
        OnSetup();
        Time.timeScale = 1;
    }

    //setup level
    private void OnSetup()
    {
        LevelManager.Instance.OnLoadLevel(levelNumber);
        LevelManager.Instance.OnLoadCharacter();
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        UIManager.Instance.OpenUI<CanvasJoystick>();
    }

    //delay time bat ui win
    IEnumerator DelayTimeVictory(float time)
    {
        yield return new WaitForSeconds(time);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasVictory>();
        Time.timeScale = 0;
    }

    //delay time bat ui fail
    IEnumerator DelayTimeFail(float time)
    {
        yield return new WaitForSeconds(time);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasFail>();
        Time.timeScale = 0;
    }
}
