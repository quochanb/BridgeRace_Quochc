using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState
{
    int targetBrick;
    //int botBrick;
    public void OnEnter(Bot bot)
    {
        targetBrick = bot.GetTargetBrick();
        Debug.Log("targetBrick: " + targetBrick);

    }

    public void OnExecute(Bot bot)
    {
        bot.BotBrick = bot.GetBrickAmount();
        Debug.Log("botBrick: " + bot.BotBrick);
        if (bot.BotBrick >= targetBrick)
        {
            bot.ChangeState(new BuildState());
        }
        else
        {
            bot.Collect();
        }
    }

    public void OnExit(Bot bot)
    {

    }    
}
