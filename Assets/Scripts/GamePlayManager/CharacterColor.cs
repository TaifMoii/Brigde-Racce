using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColor : MonoBehaviour
{
    public ColorType characterColor;   // Màu của nhân vật

    [SerializeField] private Renderer render;
    void Start()
    {
        // Gán màu trực quan theo enum (vẽ trên model)
        switch (characterColor)
        {
            case ColorType.Green:
                render.material.color = Color.green;
                break;
            case ColorType.Red:
                render.material.color = Color.red;
                break;
            case ColorType.Blue:
                render.material.color = Color.blue;
                break;
            case ColorType.Yellow:
                render.material.color = Color.yellow;
                break;
        }
    }

}
