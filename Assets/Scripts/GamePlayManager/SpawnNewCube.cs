using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewCube : MonoBehaviour
{
    public List<CubeColor> listCube = new List<CubeColor>();
    void Start()
    {
        for (int i = 0; i < listCube.Count; i++)
            listCube[i].gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < listCube.Count; i++)
            {
                if (listCube[i].cubeColor == other.GetComponent<CharacterColor>().characterColor)
                {
                    listCube[i].gameObject.SetActive(true);
                }
            }

        }
    }

}
