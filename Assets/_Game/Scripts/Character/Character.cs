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

    List<Brick> brickList = new List<Brick>();

    //khoi tao cac thong so ban dau cua character
    public virtual void OnInit()
    {
        this.ColorType = (ColorType)Random.Range(1, 7);
        ChangeColor(ColorType);
        ChangeAnim(Constants.ANIM_IDLE);
        transform.position = new Vector3(0f, 0.2f, -13f);
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
        brick.transform.localPosition = new Vector3(0, brickList.Count * 0.3f, 0);
        brickList.Add(brick);
    }

    //xoa brick di
    protected void RemoveBrick()
    {
        if(brickList.Count > 0)
        {
            Brick brick = brickList[brickList.Count - 1];
            brickList.Remove(brick);
            Destroy(brick);
        }
    }

    //xoa toan bo brick
    protected void ClearBrick()
    {
        foreach(var brick in brickList)
        {
            Destroy(brick);
        }
        brickList.Clear();
    }

    //check ground
    protected Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if(Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 0.5f;
        }
        return Tf.position;
    }

    //check di chuyen
    //protected bool CanMoving(Vector3 nextPoint)
    //{
    //    RaycastHit hit;
    //    if(Physics.Raycast(nextPoint, Vector3.down, out hit, 5f, stairLayer))
    //    {
    //        if(brickList.Count == 0)
    //        {
    //            return false;
    //        }
    //        else
    //        {
    //            //ColorType stairColorType = hit.collider.GetComponents<ColorObject>();
    //        }
    //    }
    //}

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
}
