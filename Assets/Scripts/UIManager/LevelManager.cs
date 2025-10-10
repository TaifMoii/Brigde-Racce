using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public PlayerController player;
    public Test enemy1;
    public Test enemy2;

    public Test enemy3;

    Level currentLevel;
    int loadLevel = 1;
    void Start()
    {
        UIManager.Ins.OpenMainMenu();
        LoadLevel();
    }
    public void LoadLevel()
    {
        LoadLevel(loadLevel);
        OnInit();
    }
    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[level - 1]);
    }
    public void OnInit()
    {
        player.transform.position = currentLevel.startPoint1.position;
        enemy1.transform.position = currentLevel.startPoint2.position;
        enemy2.transform.position = currentLevel.startPoint3.position;
        enemy3.transform.position = currentLevel.startPoint4.position;
        player.OnInit();
    }
    public void OnStart()
    {
        GameManager.ChangeState(GameState.Playing);
    }
    public void OnFinish()
    {
        StartCoroutine(WinCoroutine());
    }
    IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(3f);
        UIManager.Ins.OpenFinishUI();
        GameManager.ChangeState(GameState.Lose);
        MoveToChest();
    }

    public void OnNext()
    {
        loadLevel++;
        LoadLevel();
    }
    public void MoveToChest()
    {
        Vector3.MoveTowards(player.transform.position, currentLevel.finishPoint.position, 0.5f);
    }


}
