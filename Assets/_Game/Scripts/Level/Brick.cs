using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ColorObject
{
    public Stage stage;
    private Vector3 position;
    private Brick brick;
    private float respawnTime;

    public Brick(Vector3 position, Brick brick, float respawnTime)
    {
        this.position = position;
        this.brick = brick;
        this.respawnTime = respawnTime;
    }
    
    public void Collect()
    {
        stage.DespawnBrick();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.TAG_PLAYER))
        {
            Collect();
        }
    }
}


