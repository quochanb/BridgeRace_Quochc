using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform[] startPoints;
    [SerializeField] private Transform[] finishPoint;

    //lay random vi tri bat dau
    public Vector3 GetStartPoint()
    {
        int index = Random.Range(0, startPoints.Length);
        return startPoints[index].position;
    }

    //lay ra vi tri finish
    public Vector3 GetFinishPoint(int index)
    {
        return finishPoint[index].position;
    }
}
