using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Brick brickPrefab;
    [SerializeField] protected LayerMask stairLayer, groundLayer;
    [SerializeField] protected Transform brickHolder;
    [SerializeField] protected SkinnedMeshRenderer skinnedMesh;

    List<Brick> brickList = new List<Brick>();
    protected string currentAnim;

    public Stage stage;
    public Level level;

    private void Awake()
    {
        level = FindObjectOfType<Level>();
        stage = FindObjectOfType<Stage>();
        OnInit();
    }

    //khoi tao cac thong so ban dau cua character
    public virtual void OnInit()
    {
        RandomColor();
        RandomStartPoint();
        ChangeAnim(Constants.ANIM_IDLE);
    }

    //random start point cho character
    public void RandomStartPoint()
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 randomPoint = level.GetStartPoint();
        if (!points.Contains(randomPoint))
        {
            points.Add(randomPoint);
            Tf.position = randomPoint;
        }
    }

    //random mau cho character
    private void RandomColor()
    {
        List<ColorType> saveColor = new List<ColorType>();
        ColorType = (ColorType)Random.Range(1, 7);
        if (!saveColor.Contains(ColorType))
        {
            saveColor.Add(ColorType);
        }
        ChangeColor(saveColor[saveColor.Count - 1]);
    }

    //goi khi muon huy 
    public virtual void OnDespawn()
    {
        Destroy(gameObject);
    }

    //thu thap brick
    protected void AddBrick()
    {
        Brick brick = Instantiate(brickPrefab, brickHolder);
        brick.ChangeColor(ColorType);
        brick.transform.localPosition = new Vector3(0, brickList.Count * 0.33f, 0);
        brickList.Add(brick);
    }

    //xoa brick di
    protected void RemoveBrick()
    {
        if (brickList.Count > 0)
        {
            Brick brick = brickList[brickList.Count - 1];
            brickList.Remove(brick);
            Destroy(brick.gameObject);
        }
    }

    //xoa toan bo brick
    protected void ClearBrick()
    {
        foreach (Brick brick in brickList)
        {
            Destroy(brick);
        }
        brickList.Clear();
    }

    //check ground
    protected Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 5f, groundLayer))
        {
            return hit.point + Vector3.up * 0.1f;
        }
        return Tf.position;
    }

    //check di chuyen
    protected bool CanMove(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint + new Vector3(0, 1, 1), Vector3.down, out hit, 5f, stairLayer))
        {
            //character dang di len
            if (Tf.forward.z > 0)
            {
                ColorObject stairColor = hit.collider.GetComponent<ColorObject>();
                //stair cung mau
                if (stairColor.ColorType == ColorType)
                {
                    return true;
                }
                //stair khac mau
                else
                {
                    //truong hop het gach
                    if (brickList.Count == 0)
                    {
                        return false;
                    }
                    //truong hop con gach
                    else
                    {
                        RemoveBrick();
                        stairColor.ChangeColor(ColorType);
                        return true;
                    }
                }
            }
            //character di xuong
            else
            {
                return true;
            }
        }
        return true;
    }

    //lay ra so luong gach cua character
    public int GetAmountBrick()
    {
        return brickList.Count;
    }

    //thay doi anim
    protected void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    //doi mau character
    public override void ChangeColor(ColorType colorType)
    {
        skinnedMesh.material = colorData.GetMat(colorType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BRICK) && other.GetComponent<ColorObject>().ColorType == ColorType)
        {
            AddBrick();
            Destroy(other.gameObject);
        }
    }
}
