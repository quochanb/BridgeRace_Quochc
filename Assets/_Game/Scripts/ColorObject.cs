using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorObject : GameUnit
{
    [SerializeField] protected MeshRenderer meshRenderer;
    [SerializeField] protected ColorData colorData;
    private ColorType color;

    public ColorType ColorType
    {
        get { return color; }
        set { color = value; }
    }

    //thay doi mau sac
    public virtual void ChangeColor(ColorType colorType)
    {
        //this.color = colorType;
        meshRenderer.material = colorData.GetMat(colorType);
    }
}
