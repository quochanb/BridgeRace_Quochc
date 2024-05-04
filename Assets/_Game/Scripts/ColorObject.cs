using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorObject : GameUnit
{
    [SerializeField] protected ColorData colorData;
    private MeshRenderer meshRenderer;
    protected ColorType colorType;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public ColorType ColorType
    {
        get { return colorType; }
        set { colorType = value; }
    }

    //thay doi mau sac
    public virtual void ChangeColor(ColorType color)
    {
        colorType = color;
        meshRenderer.material = colorData.GetMat(color);
    }
}
