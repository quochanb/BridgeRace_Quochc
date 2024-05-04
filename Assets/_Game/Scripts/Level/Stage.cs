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
    private float[] respawnTime;

    List<Brick> bricks = new List<Brick>();
    List<Vector3> emptyBrickPoints = new List<Vector3>();

    private void Start()
    {
        OnInit();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        //for emtylist duyet tu cuoi ve dau


        //for (int i = emptyBrickPoints.Count; i > 0; i--)
        //{
        //    respawnTime[i] -= Time.deltaTime;
        //    if (respawnTime[i] < 0)
        //    {
        //        UpdateBrick(emptyBrickPoints[i]);
        //    }
        //}


        //        emtylist[indexer].time -= Time.deltaTime
        //    if (emtylist[indexer].time < 0)
        //    {
        //        sinh ra o vi tri emtylist[indexer].postion;
        //    }
    }

    public void OnInit()
    {
        //khoi tao cac vi tri sinh ra gach
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float xOffset = -(width - 1) * spaceBrick / 2f;
                float yOffset = -(height - 1) * spaceBrick / 2f;

                emptyBrickPoints.Add(new Vector3(xOffset + i * spaceBrick, 0.2f, yOffset + j * spaceBrick));
            }
        }

        //khoi tao thoi gian sinh gach
        respawnTime = new float[emptyBrickPoints.Count];
        for(int i = 0;i < respawnTime.Length; i++)
        {
            respawnTime[i] = Random.Range(1, 4);
        }
    }

    public void SpawnBrick()
    {
        for(int i = 0; i < emptyBrickPoints.Count; i++)
        {
            Vector3 position = GetRandomEmptyPosition();

            Brick brick = Instantiate(prefab, position, Quaternion.identity, spawnBrickPoint);

            brick.ColorType = (ColorType)Random.Range(1, 7);
            brick.ChangeColor(brick.ColorType);

            brick.stage = this;
            bricks.Add(brick);

            emptyBrickPoints.Remove(position);
        }
    }

    public void DespawnBrick()
    {
        Brick b = bricks[bricks.Count - 1];
        bricks.Remove(b);
    }

    private Vector3 GetRandomEmptyPosition()
    {
        return emptyBrickPoints[Random.Range(0, emptyBrickPoints.Count)];
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            SpawnBrick();
            boxCollider.enabled = false;
            other.GetComponent<Character>().stage = this;
        }
    }
}
