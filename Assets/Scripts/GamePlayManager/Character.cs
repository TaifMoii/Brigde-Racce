using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{

    public GameObject cubeHolder;
    public CharacterColor characterColor;  // script giữ màu nhân vật
    public List<GameObject> cubeLists = new List<GameObject>();
    public PlayerType playerType;



    float x = 0;
    public bool canMoving;

    void Awake()
    {
        OnInit();
    }
    void Update()
    {

    }
    public virtual void OnInit()
    {
        cubeLists.Clear();
    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Cube"))
        {
            return;
        }
        CubeColor cube = other.GetComponent<CubeColor>();
        if (cube != null)
        {
            // Chỉ nhặt Cube nếu màu trùng
            if (cube.cubeColor == characterColor.characterColor)
            {
                CubeAdd(other.gameObject);
            }
        }

    }
    public void CubeAdd(GameObject other)
    {
        GameObject cubeAdd = other.gameObject;
        canMoving = true;
        AddBrick(cubeAdd);
        StartCoroutine(WaitSpawn(other.gameObject));
        cubeLists.Add(other.gameObject);

    }
    IEnumerator WaitSpawn(GameObject cube)
    {
        cube.SetActive(false);
        yield return new WaitForSeconds(6f);
        cube.SetActive(true);
    }

    public void AddBrick(GameObject cube)
    {
        GameObject cubeAdd = Instantiate(cube,
        new Vector3(cubeHolder.transform.position.x, cubeHolder.transform.position.y + x, cubeHolder.transform.position.z)
        , transform.rotation);
        cubeAdd.transform.SetParent(cubeHolder.transform);
        x += 0.4f;
    }
    public void RemoveBrick()
    {
        if (cubeHolder.transform.childCount >= 0)
        {
            Transform lastCube = cubeHolder.transform.GetChild(cubeHolder.transform.childCount - 1);
            Destroy(lastCube.gameObject);
            x -= 0.4f;
            cubeLists.RemoveAt(cubeLists.Count - 1);
        }
        else
        {
            canMoving = false;
        }
    }
}
