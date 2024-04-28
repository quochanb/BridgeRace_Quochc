using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform[] startPoints;
    //public int index;

    public int GetIndex()
    {
        return Random.Range(0, startPoints.Length);
    }
}
