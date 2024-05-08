using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;

    private float speed = 6f;
    private int index = 0;
    private int botBrick;
    private Vector3 destination;
    private IState currentState;

    public int BotBrick
    {
        get { return botBrick; }
        set { botBrick = value; }
    }

    List<Vector3> listTarget = new List<Vector3>();

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        listTarget = stage.GetBrickPoint(this.ColorType);

        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
        }

        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    //khoi tao
    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new IdleState());
    }

    //thu thap brick
    public void Collect()
    {
        //if (listTarget.Count == 0)
        //{
        //    listTarget = stage.GetBrickPoint(this.ColorType);
        //}
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
        destination = level.GetFinishPoint();

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

    //lay ra so luong gach cua bot
    public int GetTargetBrick()
    {
        return Random.Range(3, 10);
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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Stair"))
        {
            RemoveBrick();
            BotBrick--;
            Debug.Log("Tru gach: " + BotBrick);
            other.gameObject.GetComponent<ColorObject>().ChangeColor(this.ColorType);
        }
    }
}
