using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainMenuUI;
    public GameObject finishUI;
    public GameObject gamePlayUI;
    public GameObject joystickUI;
    public GameObject loseUI;


    void Start()
    {
        OpenGamePlayUI();
    }
    public void OpenMainMenu()
    {
        mainMenuUI.SetActive(true);
        finishUI.SetActive(false);
        joystickUI.SetActive(false);
        gamePlayUI.SetActive(false);
        loseUI.SetActive(false);
    }
    public void OpenFinishUI()
    {
        mainMenuUI.SetActive(false);
        loseUI.SetActive(false);
        finishUI.SetActive(true);
        joystickUI.SetActive(false);
        gamePlayUI.SetActive(false);
    }
    public void OpenGamePlayUI()
    {
        mainMenuUI.SetActive(false);
        finishUI.SetActive(false);
        loseUI.SetActive(false);
        joystickUI.SetActive(true);
        gamePlayUI.SetActive(true);
    }
    public void OpenLoseUI()
    {
        mainMenuUI.SetActive(false);
        finishUI.SetActive(false);
        joystickUI.SetActive(false);
        gamePlayUI.SetActive(false);
        loseUI.SetActive(true);
    }

}
