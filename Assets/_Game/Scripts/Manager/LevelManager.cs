using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    public Level currentLevel;
    public static Player player;


    public void Start()
    {
        
    }

    //khoi tao trang thai bat dau game
    public void OnInit()
    {
        player.OnInit();
    }

    //reset trang thai khi ket thuc game
    public void OnReset()
    {
        player.OnDespawn();

    }

    //tao prefab level moi
    public void OnLoadLevel(int level)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[level]);
    }
}
