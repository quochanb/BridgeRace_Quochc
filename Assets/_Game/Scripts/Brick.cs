using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public ColorType color;
    public ColorData colorData;
    public MeshRenderer meshRenderer;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float width, height;

    private float spaceBrick = 3f;

    private void Start()
    {
        //SpawnBrickInLevel();
    }

    //instantiate brick tren map
    public void SpawnBrickInLevel()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float xOffset = -(width - 1) * spaceBrick / 2f;
                float yOffset = -(height - 1) * spaceBrick / 2f;

                Vector3 spawnPos = new Vector3(xOffset + i * spaceBrick, 0.25f, yOffset + j * spaceBrick);
                Instantiate(prefab, spawnPos, Quaternion.identity);
                
                ColorType colorBrick = (ColorType)Random.Range(1, 7);
                this.ChangeColor(colorBrick);
            }
        }
    }

    public void ChangeColor(ColorType colorType)
    {
        this.color = colorType;
        meshRenderer.material = colorData.GetMat(colorType);
    }
}


