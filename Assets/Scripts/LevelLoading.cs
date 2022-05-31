using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour
{
    private float timer;
    private bool uiReady;
    static public GameObject thisLevel;

    void Update()
    {
        Time.timeScale = timer;

        if (ChangeScene.onGame == 1)
        {
            timer = 0;
            uiReady = true;
        }
        else if (ChangeScene.onGame == 2)
        {
            timer = 1f;
            uiReady = false;
            ChangeScene.onGame = 0;
        }
    }
    public void OnGUI()
    {
        if (uiReady == true)
        {
            if (GUI.Button(new Rect((float)(Screen.width / 2) + 200f, (float)(Screen.height / 2) + 100f, 200f, 50f), "Начать игру"))
            {
                ChangeScene.onGame = 2;
                SceneManager.LoadScene("LoadedLevel");
                LoadLevel();               
            }
        }
    }

    public void LoadLevel()
    {
        thisLevel = Instantiate(ChangeScene.getLevel);
        thisLevel.transform.position = new Vector3(0, 0, 0);
        DontDestroyOnLoad(thisLevel);
    }
}
