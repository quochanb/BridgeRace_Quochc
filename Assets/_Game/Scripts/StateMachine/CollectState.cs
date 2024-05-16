using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState
{
    int targetBrick;

    public void OnEnter(Bot bot)
    {
        targetBrick = bot.GetTargetBrick();
    }

    public void OnExecute(Bot bot)
    {
        if (bot.GetBrickAmount() >= targetBrick)
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
