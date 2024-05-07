using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IState
{
    int botBrick;
    public void OnEnter(Bot bot)
    {
        botBrick = bot.GetBrickAmount();
        Debug.Log(botBrick);
    }

    public void OnExecute(Bot bot)
    {
        if(botBrick == 0)
        {
            bot.ChangeState(new CollectState());
        }
        else
        {
            bot.Build();
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
