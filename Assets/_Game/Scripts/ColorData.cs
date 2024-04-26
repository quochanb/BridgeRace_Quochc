using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    None = 0,
    Red = 1,
    Green = 2,
    Blue = 3,
    Yellow = 4,
    Cyan = 5,
    Pink = 6
}

[CreateAssetMenu(menuName = "ColorData")]
public class ColorData : ScriptableObject
{
    [SerializeField] Material[] materials;

    public Material GetMat(ColorType color)
    {
        return materials[(int)color];
    }
}


