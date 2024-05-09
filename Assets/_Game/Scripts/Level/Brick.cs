using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ColorObject
{
    public Stage stage;
    public Vector3 spawnPosition;
    public float respawnTime;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    //lay ra vi tri vien gach va cham voi character
    public Vector3 GetSpawnPosition()
    {
        return transform.localPosition;
    }

    //goi khi va cham voi character
    public void HitCharacter()
    {
        spawnPosition = GetSpawnPosition();
        stage.DespawnBrick(this, spawnPosition);
    }

    //change color
    public override void ChangeColor(ColorType color)
    {
        base.ChangeColor(color);
        meshRenderer.material = colorData.GetMat(colorType);
    }
}


