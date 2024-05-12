using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    [SerializeField] Character[] characters;

    private Level currentLevel;
    private CameraFollow cameraFollow;

    List<Character> characterList = new List<Character>();
    List<ColorType> characterColor = new List<ColorType>();
    List<Vector3> listStartPoint = new List<Vector3>();

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.enabled = false;
    }

    private void OnEnable()
    {
        Player.winGameEvent += SetPositionOfBot;
    }

    private void OnDisable()
    {
        Player.winGameEvent -= SetPositionOfBot;
    }

    //reset trang thai
    public void OnReset()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (characterList.Count > 0)
        {
            for (int i = 0; i < characterList.Count; i++)
            {
                Destroy(characterList[i].gameObject);
            }
            characterList.Clear();
        }
        cameraFollow.enabled = false;
    }

    //tao prefab level moi
    public void OnLoadLevel(int level)
    {
        if (level < levels.Length)
        {
            currentLevel = Instantiate(levels[level]);
            GetStartPoint();
        }
        else
        {
            Debug.LogError("No more level to load");
        }
    }

    //spawn character
    public void OnLoadCharacter()
    {
        GetCharaterColor();
        for (int i = 0; i < characters.Length; i++)
        {
            Character character = Instantiate(characters[i], listStartPoint[0], Quaternion.identity);

            listStartPoint.RemoveAt(0);
            character.ChangeColor(characterColor[0]);
            characterColor.RemoveAt(0);
            characterList.Add(character);
        }

        cameraFollow.enabled = true;
        cameraFollow.SetTarget(characterList[0].Tf);
    }

    //lay mau cho character
    private void GetCharaterColor()
    {
        while (characterColor.Count < characters.Length)
        {
            ColorType color = (ColorType)Random.Range(1, 7);
            if (!characterColor.Contains(color))
            {
                characterColor.Add(color);
            }
        }
    }

    //lay vi tri start point
    private void GetStartPoint()
    {
        while (listStartPoint.Count < characters.Length)
        {
            Vector3 point = currentLevel.GetStartPoint();
            if (!listStartPoint.Contains(point))
            {
                listStartPoint.Add(point);
            }
        }
    }

    //set vi tri bot khi player win game
    public void SetPositionOfBot()
    {
        for(int i = 1;i < characters.Length;i++)
        {
            characterList[i].GetComponent<Bot>().DeactiveNavmesh();
        }
        
        characterList[1].Tf.position = currentLevel.GetFinishPoint(1);
        characterList[2].Tf.position = currentLevel.GetFinishPoint(2);
    }
}
