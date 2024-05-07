using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : ColorObject
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ChangeColor(ColorType.None);
    }

    public override void ChangeColor(ColorType color)
    {
        base.ChangeColor(color);
        meshRenderer.material = colorData.GetMat(colorType);
    }
}
