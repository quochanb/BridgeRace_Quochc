using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Brick prefab;
    [SerializeField] private float width, height;
    [SerializeField] private Transform spawnBrickPoint;

    private float spaceBrick = 3f;

    List<Brick> bricks = new List<Brick>();

    public void SpawnBrick()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float xOffset = -(width - 1) * spaceBrick / 2f;
                float yOffset = -(height - 1) * spaceBrick / 2f;

                Brick brick = Instantiate(prefab, spawnBrickPoint);
                brick.transform.position = new Vector3(xOffset + i * spaceBrick, 0.25f, yOffset + j * spaceBrick);

                //colorType = (ColorType)Random.Range(1, 7);
                //brick.ChangeColor(colorType);

                brick.stage = this;
                bricks.Add(brick);
            }
        }
    }

    public void DespawnBrick(Brick b)
    {
        bricks.Remove(b);
    }

    //getbrickpoint
}
