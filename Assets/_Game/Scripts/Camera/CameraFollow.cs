using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;

    void LateUpdate()
    {
        Vector3 camPos = currentTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, camPos, smoothTime);
        transform.LookAt(currentTarget);
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }
}
