using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : ColorObject
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void ChangeColor(ColorType color)
    {
        base.ChangeColor(color);
        meshRenderer.material = colorData.GetMat(colorType);
    }
}
