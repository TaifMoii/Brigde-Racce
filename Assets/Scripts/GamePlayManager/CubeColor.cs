using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType
{
    Green,
    Red,
    Blue,
    Yellow,
    None
}
public enum PlayerType
{
    Player,
    Enemy
}

public class CubeColor : MonoBehaviour
{
    public ColorType cubeColor; // mỗi Cube sẽ có 1 enum riêng
    [HideInInspector] public bool isCollected;
    void Awake()
    {
        Init();
    }
    public void Init()
    {
        isCollected = false;
    }
}
