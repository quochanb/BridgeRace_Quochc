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
            emptyList[i].RespawnTime -= Time.deltaTime;
            if (emptyList[i].RespawnTime < 0)
            {
                SpawnBrick(emptyList[i].Position);
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

    //tao va them gach vao list
    public void SpawnBrick(Vector3 position)
    {
        position = GetRandomEmptyPosition();

        Brick brick = Instantiate(prefab, position, Quaternion.identity, spawnBrickPoint);

        brick.ColorType = (ColorType)Random.Range(1, 7);
        brick.ChangeColor(brick.ColorType);

        brick.stage = this;
        bricks.Add(brick);

        emptyBrickPoints.Remove(position);
    }

    //xoa gach khoi list
    public void DespawnBrick(Brick b)
    {
        bricks.Remove(b);
    }

    //lay ra vi tri trong ngau nhien
    private Vector3 GetRandomEmptyPosition()
    {
        return emptyBrickPoints[Random.Range(0, emptyBrickPoints.Count)];
    }

    //them vi tri ko co gach vao list moi
    public void AddEmptyBrickPoint(Vector3 position, float time)
    {
        emptyList.Add(new Vector3AndTime(position, time));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            for (int i = emptyBrickPoints.Count - 1; i >= 0; i--)
            {
                SpawnBrick(emptyBrickPoints[i]);

            }
            boxCollider.enabled = false;
            other.GetComponent<Character>().stage = this;
        }
    }
}
