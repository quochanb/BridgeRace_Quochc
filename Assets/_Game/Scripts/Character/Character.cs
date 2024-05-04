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

    //khoi tao cac thong so ban dau cua character
    public virtual void OnInit()
    {
        ColorType = (ColorType)Random.Range(1,7);
        ChangeColor(ColorType);
        ChangeAnim(Constants.ANIM_IDLE);
        transform.position = new Vector3(0f, 0.15f, -13f);
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
        foreach (var brick in brickList)
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
