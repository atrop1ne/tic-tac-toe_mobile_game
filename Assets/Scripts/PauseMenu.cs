using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private float timer;
    private bool onPause;
    private bool uiPause;

    public void ClickPause()
    {
        onPause = true;
    }
    void Update()
    {
        Time.timeScale = timer;

        if (onPause == true)
        {
            timer = 0;
            uiPause = true;

        }
        else if (onPause == false)
        {
            timer = 1f;
            uiPause = false;
        }
    }
    public void OnGUI()
    {
        if (uiPause == true)
        {
            if (GUI.Button(new Rect((float)(Screen.width / 2) - 100f, (float)(Screen.height / 2) - 50f, 200f, 50f), "Продолжить"))
            {
                onPause = false;
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2) - 100f, (float)(Screen.height / 2), 200f, 50f), "Вернуться на карту"))
            {
                Destroy(LevelLoading.thisLevel);
                SceneManager.LoadScene("Map");
            }
        }
    }
}
