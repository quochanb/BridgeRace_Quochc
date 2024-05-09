using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    [SerializeField] Character[] characters;

    public Level currentLevel;

    private CameraFollow cameraFollow;

    List<Character> characterList = new List<Character>();
    List<ColorType> characterColor = new List<ColorType>();
    List<Vector3> listStartPoint = new List<Vector3>();

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.enabled = false;
    }

    //reset trang thai khi ket thuc game
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
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[level]);

        GetStartPoint();
    }

    //spawn character
    public void OnLoadCharacter()
    {
        if (characterList.Count > 0)
        {
            for (int i = 0; i < characterList.Count; i++)
            {
                Destroy(characterList[i].gameObject);
            }
            characterList.Clear();
        }
        else
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
}
