using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public ColorType color;
    public ColorData colorData;
    public SkinnedMeshRenderer meshRenderer;
    [SerializeField] protected Animator anim;
    [SerializeField] private Transform brickHolder;
    [SerializeField] private GameObject brickPrefab;

    private string currentAnim;

    List<GameObject> brickList = new List<GameObject>();

    //khoi tao cac thong so ban dau cua character
    public virtual void OnInit()
    {
        color = (ColorType)Random.Range(1, 7);
        ChangeColor(color);

        transform.position = new Vector3(0f, 0.1f, -13f);
    }

    //goi khi muon huy 
    public virtual void OnDespawn()
    {

    }

    //thu thap brick
    protected virtual void AddBrick()
    {
        GameObject brick = Instantiate(brickPrefab, brickHolder);
        brick.transform.localPosition = new Vector3(0, brickList.Count * 0.3f, 0);
        brickList.Add(brick);
    }

    //xoa brick di
    protected virtual void RemoveBrick()
    {
        if(brickList.Count > 0)
        {
            GameObject brick = brickList[brickList.Count - 1];
            brickList.Remove(brick);
            Destroy(brick);
        }
    }

    //xoa toan bo brick
    protected virtual void ClearBrick()
    {
        foreach(var brick in brickList)
        {
            Destroy(brick);
        }
        brickList.Clear();
    }

    //thay doi anim
    protected virtual void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    //thay doi mau sac
    public void ChangeColor(ColorType colorType)
    {
        this.color = colorType;
        meshRenderer.material = colorData.GetMat(colorType);
    }
}
