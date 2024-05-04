using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform[] startPoints;
    [SerializeField] private Transform finishPoint;


    public int GetIndex()
    {
        return Random.Range(0, startPoints.Length);
    }
}
