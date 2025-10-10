using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform finishPoint;
    public Transform finishPoint1;

    public bool isFinish;


    void Awake()
    {

        isFinish = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character character = other.GetComponent<Character>();
            float dist = Vector3.Distance(other.transform.position, finishPoint.position);
            if (dist <= 1.5f)
            {
                if (character.playerType == PlayerType.Player)
                {
                    other.transform.position = finishPoint1.position;
                    isFinish = true;
                    UIManager.Ins.OpenFinishUI();
                }
                if (character.playerType == PlayerType.Enemy)
                {
                    other.transform.position = finishPoint1.position;
                    isFinish = true;
                    UIManager.Ins.OpenLoseUI();
                }

            }

        }
    }

}
