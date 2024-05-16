using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Brick prefab;
    [SerializeField] private float width, height;
    [SerializeField] private Transform spawnBrickPoint;

    private float spaceBrick = 2.2f;
    private bool isCollided = false;
    private int[] brickCount = new int[10];
    private ColorType color;

    List<Brick> bricks = new List<Brick>();
    List<Vector3> spawnBrickPointList = new List<Vector3>();
    [SerializeField] List<ColorType> colorList = new List<ColorType>();
    [SerializeField] List<Vector3AndTime> emptyBrickPointList = new List<Vector3AndTime>();

    private void Awake()
    {
        OnInit();
    }

    private void Update()
    {
        //respawn brick
        for (int i = emptyBrickPointList.Count - 1; i >= 0; i--)
        {
            //giam time qua cac frame
            emptyBrickPointList[i].RespawnTime -= Time.deltaTime;

            if (emptyBrickPointList[i].RespawnTime < 0)
            {
                SpawnBrick(emptyBrickPointList[i].Position);
                emptyBrickPointList.RemoveAt(i);
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

                spawnBrickPointList.Add(new Vector3(xOffset + i * spaceBrick, 0.3f, yOffset + j * spaceBrick));
            }
        }
    }

    //spawn brick
    public void SpawnBrick(Vector3 position)
    {
        if (colorList.Count == 0)
        {
            return;
        }
        //lay random vi tri
        position = GetRandomEmptyPosition();
        Brick brick = Instantiate(prefab, position, Quaternion.identity, this.spawnBrickPoint);
        //set lai local position
        brick.Tf.localPosition = position;
        //random mau cho brick
        int index = Random.Range(0, colorList.Count);
        color = colorList[index];
        brick.ChangeColor(color);
        //gan brick cho stage hien tai
        brick.stage = this;
        //add brick vao list de quan ly
        bricks.Add(brick);
        //xoa vi tri vua sinh ra brick
        spawnBrickPointList.Remove(position);

        //dem so luong gach tung mau
        brickCount[(int)color]++;
        if (brickCount[(int)color] >= width * height / 5)
        {
            colorList.Remove(color);
        }
    }

    //goi khi character va cham voi brick
    public void DespawnBrick(Brick b, Vector3 spawnPosition)
    {
        //xoa brick trong list
        bricks.Remove(b);
        //giam so luong gach trong mang
        brickCount[(int)b.ColorType]--;
        //add lai color
        if (!colorList.Contains(b.ColorType))
        {
            colorList.Add(b.ColorType);
        }

        float respawnTime = Random.Range(4f, 7f);
        AddEmptyBrickPoint(spawnPosition, respawnTime);
    }

    //goi khi character sang stage moi
    public void ClearBrick(ColorType colorType)
    {
        //xoa het gach trong brick list
        for (int i = bricks.Count - 1; i >= 0; i--)
        {
            if (bricks[i].ColorType == colorType)
            {
                Destroy(bricks[i].gameObject);
                bricks.RemoveAt(i);
            }
        }
        //xoa mau trong color list
        for (int i = 0; i < colorList.Count; i++)
        {
            if (colorList[i] == colorType)
            {
                colorList.RemoveAt(i);
            }
        }

        emptyBrickPointList.Clear();
    }

    //them vi tri ko co brick vao list moi va list ban dau
    public void AddEmptyBrickPoint(Vector3 position, float time)
    {
        emptyBrickPointList.Add(new Vector3AndTime(position, time));
        spawnBrickPointList.Add(position);

    }

    //lay ra list vi tri brick co mau chi dinh
    public List<Vector3> GetBrickPoint(ColorType colorType)
    {
        List<Vector3> list = new List<Vector3>();
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].ColorType == colorType)
            {
                list.Add(bricks[i].Tf.position);
            }
        }

        return list;
    }

    //luu color cua charater
    public void GetColorCharacter(ColorType colorType)
    {
        if (!colorList.Contains(colorType))
        {
            colorList.Add(colorType);
        }
    }

    //lay vi tri ngau nhien de spawn brick
    private Vector3 GetRandomEmptyPosition()
    {
        return spawnBrickPointList[Random.Range(0, spawnBrickPointList.Count)];
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            isCollided = true;
            this.GetColorCharacter(other.GetComponent<Character>().ColorType);
            
            if (isCollided)
            {
                other.GetComponent<Character>().stage = this;

                for (int i = spawnBrickPointList.Count - 1; i >= 0 && colorList.Count != 0; i--)
                {
                    SpawnBrick(spawnBrickPointList[i]);
                }
                isCollided = false;
            }
        }
    }
}
