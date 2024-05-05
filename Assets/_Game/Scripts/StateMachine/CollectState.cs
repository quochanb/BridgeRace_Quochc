using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState
{
    int targetBrick;
    int botBrick;
    public void OnEnter(Bot bot)
    {
        targetBrick = Random.Range(3, 9);
        botBrick = bot.GetAmountBrick();
    }

    public void OnExecute(Bot bot)
    {
        if(botBrick >= targetBrick)
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
