using UnityEngine;

public class Vector3AndTime
{
    private Vector3 position;
    private float respawnTime;

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    public float RespawnTime
    {
        get { return respawnTime; }
        set { respawnTime = value; }
    }

    public Vector3AndTime(Vector3 position, float time)
    {
        Position = position;
        RespawnTime = time;
    }
}
