using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;

    private Vector3 destination;
    private IState currentState;

    private void Update()
    {
        if (currentState != null)
        {
            //Debug.Log(currentState);
            currentState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }

    //thu thap brick
    public void Collect()
    {
        if (IsDestination)
        {
            Vector3 destination = stage.GetBrickPoint(this.ColorType);
            SetDestination(destination);
        }
        ChangeAnim(Constants.ANIM_RUN);
    }

    //xay cau
    public void Build()
    {
        Vector3 destination = level.GetFinishPoint();
        SetDestination(destination);
        ChangeAnim(Constants.ANIM_RUN);
    }

    //kiem tra xem da den muc tieu chua
    public bool IsDestination => Vector3.Distance(Tf.position, destination + (Tf.position.y - destination.y) * Vector3.up) < 0.1f;

    //set diem den
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    //change state
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
