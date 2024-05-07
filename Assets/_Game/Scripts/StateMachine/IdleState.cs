using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float delayTime;
    float timer;
    public void OnEnter(Bot bot)
    {
        delayTime = 1;
        timer = 0;
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if(timer >= delayTime)
        {
            bot.ChangeState(new CollectState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
