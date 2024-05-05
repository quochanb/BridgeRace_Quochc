using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform[] startPoints;
    [SerializeField] private Transform finishPoint;

    public Vector3 GetStartPoint()
    {
        int index = Random.Range(0, startPoints.Length);
        Debug.Log("index: " + index);
        Debug.Log("length: " + startPoints.Length);
        return startPoints[index].position;
    }

    public Vector3 GetFinishPoint()
    {
        return finishPoint.position;
    }

    public int Number()
    {
        return 100;
    }
}
