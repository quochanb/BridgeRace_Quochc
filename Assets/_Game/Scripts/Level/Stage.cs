using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Brick prefab;
    [SerializeField] private float width, height;
    [SerializeField] private Transform spawnBrickPoint;

    private BoxCollider boxCollider;
    private float spaceBrick = 3f;
    private int[] brickCounts = new int[6];
    private int totalBricks = 0;

    List<Brick> bricks = new List<Brick>();
    List<Vector3> emptyBrickPoints = new List<Vector3>();
    List<Vector3AndTime> emptyList = new List<Vector3AndTime>();

    private void Awake()
    {
        OnInit();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        for (int i = emptyList.Count - 1; i >= 0; i--)
        {
            //giam time qua cac frame
            emptyList[i].RespawnTime -= Time.deltaTime;
            if (emptyList[i].RespawnTime < 0)
            {
                SpawnBrick(emptyList[i].Position);
                emptyList.RemoveAt(i);
            }
        }
    }

    //khoi tao vi tri cac vien gach
    public void OnInit()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float xOffset = -(width - 1) * spaceBrick / 2f;
                float yOffset = -(height - 1) * spaceBrick / 2f;

                emptyBrickPoints.Add(new Vector3(xOffset + i * spaceBrick, 0.3f, yOffset + j * spaceBrick));
            }
        }
    }

    //spawn brick
    public void SpawnBrick(Vector3 position)
    {
        //lay random vi tri
        position = GetRandomEmptyPosition();
        Brick brick = Instantiate(prefab, position, Quaternion.identity, this.spawnBrickPoint);
        //set lai local position
        brick.Tf.localPosition = position;
        //random mau cho brick
        ColorType colorType = (ColorType)Random.Range(1, 7);
        //brick.ColorType = colorType;
        brick.ChangeColor(colorType);
        //gan brick cho stage hien tai
        brick.stage = this;
        //add brick vao list de quan ly
        bricks.Add(brick);
        //xoa vi tri vua sinh ra brick trong empty list
        emptyBrickPoints.Remove(position);

        //tang so luong brick tung mau
        //brickCounts[(int)colorType]++;
        totalBricks++;
    }

    //xoa brick khoi list
    public void DespawnBrick(Brick b)
    {
        bricks.Remove(b);
    }

    //them vi tri ko co brick vao list moi va list ban dau
    public void AddEmptyBrickPoint(Vector3 position, float time)
    {
        emptyList.Add(new Vector3AndTime(position, time));
        emptyBrickPoints.Add(position);
    }

    //lay ra vi tri brick co mau chi dinh
    public Vector3 GetBrickPoint(ColorType colorType)
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].ColorType == colorType)
            {
                return bricks[i].Tf.position;
            }
        }
        return Vector3.zero;
    }

    //lay vi tri ngau nhien de spawn brick
    private Vector3 GetRandomEmptyPosition()
    {
        return emptyBrickPoints[Random.Range(0, emptyBrickPoints.Count)];
    }

    //lay random mau cho brick
    //private ColorType GetRandomBrickColor()
    //{
    //    List<ColorType> saveColor = new List<ColorType>();
    //    for (int i = 0; i < brickCounts.Length; i++)
    //    {
    //        if (brickCounts[i] == 0)
    //            saveColor.Add((ColorType)(i + 1));
    //        Debug.Log((ColorType)(i + 1));
    //    }
    //    return saveColor[Random.Range(0, saveColor.Count)];
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            other.GetComponent<Character>().stage = this;
            for (int i = emptyBrickPoints.Count - 1; i >= 0; i--)
            {
                SpawnBrick(emptyBrickPoints[i]);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            this.boxCollider.enabled = false;
        }
    }
}
