using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    [SerializeField]
    private GameObject GameOverScreen;
    [SerializeField]
    private GameObject LevelCompleteScreen;
    [SerializeField]
    private GameObject PauseMenuScreen;

    public void ShowGameOverScreen()
    {
        GameOverScreen.SetActive(true);
    }

    public void ShowLevelCompleteScreen()
    {
        LevelCompleteScreen.SetActive(true);
    }

    public void ShowPauseMenuScreen()
    {
        PauseMenuScreen.SetActive(true);
    }

    public void HidePauseMenuScreen()
    {
        PauseMenuScreen.SetActive(false);
    }

    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
}
