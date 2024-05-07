using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;

    private Vector3 destination;
    private IState currentState;
    private int index = 0;

    private int targetBrick;
    public int TargetBrick => targetBrick;

    List<Vector3> listTarget = new List<Vector3>();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        listTarget = stage.GetBrickPoint(this.ColorType);
        Debug.Log("list: " +  listTarget.Count);
        
    }

    private void Update()
    {
        if (currentState != null)
        {
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
        
        SetDestination(listTarget[index]);
        if (IsDestination)
        {
            SetDestination(listTarget[index++]);
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

    public int GetTargetBrick()
    {
        return Random.Range(3, 10);
    }

    //public void GetListTarget()
    //{
    //    targetBrick = GetTargetBrick();
    //    for (int i = 0; i < targetBrick; i++)
    //    {
    //        listTarget = stage.GetBrickPoint(this.ColorType);
            
    //        Debug.Log("sl: " + listTarget.Count);
    //    }
    //}

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
