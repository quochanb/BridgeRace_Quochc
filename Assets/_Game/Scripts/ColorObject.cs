using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorObject : GameUnit
{
    [SerializeField] protected ColorData colorData;
    private MeshRenderer meshRenderer;
    protected ColorType color;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public ColorType ColorType
    {
        get { return color; }
        set { color = value; }
    }

    //thay doi mau sac
    public virtual void ChangeColor(ColorType colorType)
    {
        meshRenderer.material = colorData.GetMat(colorType);
    }
}
