using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Brick prefab;
    [SerializeField] private float width, height;
    [SerializeField] private Transform spawnBrickPoint;

    private float spaceBrick = 2f;
    private bool isCollided = false;
    private ColorType color;
    private int[] brickCount = new int[10];

    List<Brick> bricks = new List<Brick>();
    List<Vector3> emptyBrickPoints = new List<Vector3>();
    [SerializeField] List<Vector3AndTime> emptyList = new List<Vector3AndTime>();
    [SerializeField] List<ColorType> listColor = new List<ColorType>();

    private void Awake()
    {
        OnInit();
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
        if (listColor.Count == 0)
            return;
        //lay random vi tri
        position = GetRandomEmptyPosition();
        Brick brick = Instantiate(prefab, position, Quaternion.identity, this.spawnBrickPoint);
        //set lai local position
        brick.Tf.localPosition = position;
        //random mau cho brick
        int index = Random.Range(0, listColor.Count);
        color = listColor[index];
        brick.ChangeColor(color);
        //gan brick cho stage hien tai
        brick.stage = this;
        //add brick vao list de quan ly
        bricks.Add(brick);
        //xoa vi tri vua sinh ra brick trong empty list
        emptyBrickPoints.Remove(position);

        brickCount[(int)color]++;
        if (brickCount[(int)color] >= width * height / 5)
        {
            listColor.Remove(color);
        }
    }

    //goi khi character va cham voi brick
    public void DespawnBrick(Brick b,Vector3 spawnPosition)
    {
        bricks.Remove(b);
        brickCount[(int)b.ColorType]--;
        if (!listColor.Contains(b.ColorType))
        {
            listColor.Add(b.ColorType);
        }
        float respawnTime = Random.Range(2f, 5f);
        
        AddEmptyBrickPoint(spawnPosition, respawnTime);
    }

    public void ClearBrick(ColorType colorType)
    {
        for(int i = 0; i < bricks.Count; i++)
        {
            if(bricks[i].ColorType == colorType)
            {
                //Destroy(bricks[i].gameObject);
                bricks.RemoveAt(i);
            }
        }
        emptyList.Clear();
    }

    //them vi tri ko co brick vao list moi va list ban dau
    public void AddEmptyBrickPoint(Vector3 position, float time)
    {
        emptyList.Add(new Vector3AndTime(position, time));
        emptyBrickPoints.Add(position);

    }

    //lay ra vi tri brick co mau chi dinh
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
        if (!listColor.Contains(colorType))
        {
            listColor.Add(colorType);
        }
    }

    //lay vi tri ngau nhien de spawn brick
    private Vector3 GetRandomEmptyPosition()
    {
        return emptyBrickPoints[Random.Range(0, emptyBrickPoints.Count)];
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
                for (int i = emptyBrickPoints.Count - 1; i >= 0 && listColor.Count != 0; i--)
                {
                    SpawnBrick(emptyBrickPoints[i]);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            isCollided = false;
        }
    }
}
