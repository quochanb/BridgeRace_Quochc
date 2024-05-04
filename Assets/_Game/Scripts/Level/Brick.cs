using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ColorObject
{
    public Stage stage;

    
    public Vector3 GetBrickPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            stage.DespawnBrick(this);
        }
    }
}


