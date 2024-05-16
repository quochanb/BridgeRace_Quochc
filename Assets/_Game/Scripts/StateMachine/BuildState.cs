using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IState
{
    public void OnEnter(Bot bot)
    {

    }

    public void OnExecute(Bot bot)
    {
        if(bot.BotBrick <= 0)
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
