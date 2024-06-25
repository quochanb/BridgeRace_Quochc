using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorObject : GameUnit
{
    [SerializeField] protected ColorData colorData;

    protected ColorType colorType;

    public ColorType ColorType
    {
        get { return colorType; }
        set { colorType = value; }
    }

    //thay doi mau sac
    public virtual void ChangeColor(ColorType color)
    {
        colorType = color;
    }
}
