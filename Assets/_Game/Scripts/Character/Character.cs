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

    protected string currentAnim;

    public Stage stage;
    public Level level;

    List<Brick> brickList = new List<Brick>();

    private void Awake()
    {
        level = FindObjectOfType<Level>();
        stage = FindObjectOfType<Stage>();
        OnInit();
    }

    //khoi tao
    public virtual void OnInit()
    {
        ChangeAnim(Constants.ANIM_IDLE);
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
            return hit.point + Vector3.up * 0.3f;
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
                    //het gach
                    if (brickList.Count == 0)
                    {
                        return false;
                    }
                    //con gach
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
    public int GetBrickAmount()
    {
        return brickList.Count;
    }

    //change anim
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
        base.ChangeColor(colorType);
        skinnedMesh.material = colorData.GetMat(colorType);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BRICK) && other.GetComponent<ColorObject>().ColorType == ColorType)
        {
            other.GetComponent<Brick>().HitCharacter();
            AddBrick();
            Destroy(other.gameObject);
        }

        if (other.CompareTag(Constants.TAG_ENDBOX))
        {
            stage.ClearBrick(this.ColorType);
            other.tag = Constants.TAG_UNTAGGED;
        }

        if (other.CompareTag(Constants.TAG_CYLINDER))
        {
            other.GetComponent<ColorObject>().ChangeColor(this.ColorType);
        }

        if (other.CompareTag(Constants.TAG_FINISH) || other.CompareTag(Constants.TAG_ORDERBOX))
        {
            Tf.rotation = Quaternion.LookRotation(Vector3.back);

            for (int i = brickList.Count - 1; i >= 0; i--)
            {
                if (brickList[i] != null)
                {
                    Destroy(brickList[i].gameObject);
                }
            }

            other.GetComponent<ColorObject>().ChangeColor(this.ColorType);
        }
    }
}
