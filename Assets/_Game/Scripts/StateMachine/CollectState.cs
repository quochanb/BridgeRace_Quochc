using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState
{
    int targetBrick;
    int botBrick;
    public void OnEnter(Bot bot)
    {
        targetBrick = bot.GetTargetBrick();
    }

    public void OnExecute(Bot bot)
    {
        botBrick = bot.GetBrickAmount();
        if (botBrick >= targetBrick)
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
