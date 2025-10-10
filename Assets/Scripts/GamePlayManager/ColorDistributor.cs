using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDistributor : MonoBehaviour
{
    public List<GameObject> cubes; // Kéo 40 Cube vào Inspector

    void Start()
    {
        // if (cubes.Count != 40)
        // {
        //     Debug.LogWarning("Bạn cần đúng 40 Cube để chia đều!");
        //     return;
        // }

        // Tạo list 10 Green, 10 Red, 10 Blue, 10 Yellow
        List<ColorType> colorPool = new List<ColorType>();
        for (int i = 0; i < 10; i++)
        {
            colorPool.Add(ColorType.Green);
            colorPool.Add(ColorType.Red);
            colorPool.Add(ColorType.Blue);
            colorPool.Add(ColorType.Yellow);
        }

        // Trộn ngẫu nhiên danh sách màu
        Shuffle(colorPool);

        // Gán màu cho từng Cube theo danh sách đã trộn
        for (int i = 0; i < cubes.Count; i++)
        {
            CubeColor cc = cubes[i].GetComponent<CubeColor>();
            if (cc == null) cc = cubes[i].AddComponent<CubeColor>();

            cc.cubeColor = colorPool[i];

            // đổi màu trực quan
            switch (cc.cubeColor)
            {
                case ColorType.Green:
                    cubes[i].GetComponent<Renderer>().material.color = Color.green;
                    break;
                case ColorType.Red:
                    cubes[i].GetComponent<Renderer>().material.color = Color.red;
                    break;
                case ColorType.Blue:
                    cubes[i].GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case ColorType.Yellow:
                    cubes[i].GetComponent<Renderer>().material.color = Color.yellow;
                    break;
            }
        }
    }

    // Hàm trộn list Fisher–Yates shuffle
    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
