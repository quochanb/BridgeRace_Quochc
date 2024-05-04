using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ColorObject
{
    public Stage stage;
    public Vector3 spawnPosition;
    public float respawnTime;

    public Vector3 GetSpawnPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            spawnPosition = GetSpawnPosition();
            respawnTime = Random.Range(3, 6);
            stage.AddEmptyBrickPoint(spawnPosition, respawnTime);
            stage.DespawnBrick(this);
        }
    }
}


