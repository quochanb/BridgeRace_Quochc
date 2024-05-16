using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;

    private int index = 0;
    private int botBrick = 0;
    private bool isStair = false;
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
        if (GameManager.Instance.currentState == GameState.GamePlay)
        {
            agent.isStopped = false;
            listTarget = stage.GetBrickPoint(this.ColorType);
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }
        else if (GameManager.Instance.currentState == GameState.Victory)
        {
            return;
        }
        else
        {
            agent.isStopped = true;
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
        if (index >= listTarget.Count)
        {
            return;
        }
        SetDestination(listTarget[index]);
        if (IsDestination)
        {
            SetDestination(listTarget[index++]);
            listTarget.RemoveAt(index);
        }
        ChangeAnim(Constants.ANIM_RUN);
    }

    //xay bac thang
    public void Build()
    {
        isStair = CheckStair();
        if (isStair)
        {
            destination = level.GetFinishPoint(0);
            SetDestination(destination);
            ChangeAnim(Constants.ANIM_RUN);
        }
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
        return Random.Range(10, 16);
    }

    //check dieu kien di chuyen
    public bool CheckStair()
    {
        RaycastHit hit;
        Debug.DrawRay(Tf.position + Vector3.up, Vector3.down + Vector3.forward, Color.red, 3f);
        if (Physics.Raycast(Tf.position + Vector3.up, Vector3.down + Vector3.forward, out hit, 5f, stairLayer))
        {
            ColorObject stairColor = hit.collider.GetComponent<ColorObject>();
            if (stairColor.ColorType == this.ColorType)
            {
                return true;
            }
            else
            {
                RemoveBrick();
                BotBrick--;
                stairColor.ChangeColor(this.ColorType);
                return false;
            }
        }
        return true;
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

    //goi khi player win game
    public void DeactiveNavmesh()
    {
        agent.isStopped = true;
        agent.enabled = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(Constants.TAG_FINISH))
        {
            ChangeAnim(Constants.ANIM_DANCE);
            Tf.position = level.GetFinishPoint(0);
            GameManager.Instance.OnFail();
        }

        if (other.CompareTag(Constants.TAG_ORDERBOX))
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }
}
