using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    [SerializeField] Character[] characters;
    public Level currentLevel;
    public Player player;

    private CameraFollow cameraFollow;

    //khoi tao trang thai bat dau game
    public void OnInit()
    {
        
    }

    //reset trang thai khi ket thuc game
    public void OnReset()
    {

    }

    //tao prefab level moi
    public void OnLoadLevel(int level)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        
        currentLevel = Instantiate(levels[level], Vector3.zero, Quaternion.identity);
    }
}
