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
    Resume = 6,
    Continue = 7
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
        levelNumber = PlayerPrefs.GetInt("game_level", 0);
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
            case GameState.Continue:
                OnContinue();
                break;
            default:
                break;
        }
    }

    //state main menu
    public void OnMainMenu()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>().SetState(levelNumber);
        LevelManager.Instance.OnReset();
        Time.timeScale = 0;
    }

    //state play game
    public void OnGamePlay()
    {
        levelNumber = 0;
        OnSetup();
    }

    //state setting
    public void OnSetting()
    {
        Time.timeScale = 0;
    }

    //state victory
    public void OnVictory()
    {
        //UIManager.Instance.CloseUI<CanvasJoystick>(0);
        StartCoroutine(DelayTimeVictory(2));
    }

    //state fail
    public void OnFail()
    {
        int currentNumberLevel = levelNumber;
        PlayerPrefs.SetInt("game_level", currentNumberLevel);
        StartCoroutine(DelayTime(1));
        UIManager.Instance.OpenUI<CanvasFail>();
        Time.timeScale = 0;
    }

    //state next
    private void OnNextLevel()
    {
        levelNumber++;
        PlayerPrefs.SetInt("game_level", levelNumber);
        OnSetup();
    }

    //state resume
    private void OnResume()
    {
        levelNumber = PlayerPrefs.GetInt("game_level");
        OnSetup();
    }

    //state continue
    private void OnContinue()
    {
        Time.timeScale = 1;
    }

    //setup level
    private void OnSetup()
    {
        LevelManager.Instance.OnReset();
        LevelManager.Instance.OnLoadLevel(levelNumber);
        LevelManager.Instance.OnLoadCharacter();
        Time.timeScale = 1;
    }

    //delay time
    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator DelayTimeVictory(float time)
    {
        yield return new WaitForSeconds(time);
        UIManager.Instance.OpenUI<CanvasVictory>();
        //Time.timeScale = 0;
    }
}
