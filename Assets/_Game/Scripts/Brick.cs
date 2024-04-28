using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ColorObject
{
    public Stage stage;
    
    public void Collect()
    {
        stage.DespawnBrick(this);
    }
}


