using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Stair : MonoBehaviour
{
    public GameObject stair;
    public GameObject wall;
    public ColorType stairColor;
    public bool filled;
    public bool playerOnStair;

    void Awake()
    {
        stair.SetActive(false);
        filled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            Character character = other.GetComponent<Character>();
            ColorType color = character.characterColor.characterColor;
            if (character == null)
            {
                return;
            }

            if (character.cubeHolder.transform.childCount <= 0)
            {
                if (character.transform.position.y < stair.transform.position.y)
                {
                    if (color != stairColor)
                    {
                        playerOnStair = false;
                    }
                    else
                    {
                        playerOnStair = true;
                    }
                }
                return;
            }

            if (color != stairColor)
            {
                character.RemoveBrick();
                stairColor = color;
                stair.SetActive(true);
                ChangeColor(stairColor);
                filled = true;
                if (character.playerType == PlayerType.Player)
                {
                    if (player.isGrounded && filled)
                    {
                        playerOnStair = true;
                        if (character.cubeHolder.transform.childCount > 0)
                        {
                            wall.SetActive(false);
                        }
                    }
                }
            }
        }
    }
    void Update()
    {
        if (!playerOnStair)
        {
            wall.SetActive(true);
        }
    }

    public void ChangeColor(ColorType newColor)
    {
        Renderer renderer = stair.GetComponent<Renderer>();
        stairColor = newColor;
        // Gán màu trực quan theo enum (vẽ trên model)
        switch (stairColor)
        {
            case ColorType.Green:
                renderer.material.color = Color.green;
                break;
            case ColorType.Red:
                renderer.material.color = Color.red;
                break;
            case ColorType.Blue:
                renderer.material.color = Color.blue;
                break;
            case ColorType.Yellow:
                renderer.material.color = Color.yellow;
                break;
        }
    }

}
