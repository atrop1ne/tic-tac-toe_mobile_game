using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private float gameTimer;
    private bool onPause;
    private bool uiPause;
    private bool enDeath;
    private bool heDeath;

    [SerializeField]
    private Texture play;
    [SerializeField]
    private Texture home;
    [SerializeField]
    private Texture death;
    [SerializeField]
    private Texture win;

    public void ClickPause()
    {
        onPause = true;
    }
    void Update()
    {
        Time.timeScale = gameTimer;

        if (onPause == true)
        {
            gameTimer = 0;
            uiPause = true;
        }
        else if (onPause == false)
        {
            gameTimer = 1f;
            uiPause = false;
        }
        if (EnemyController.instance.enemyDeath)
        {
            gameTimer = 0;
            enDeath = true;
        }
        if (HeroController.instance.heroDeath)
        {
            gameTimer = 0;
            heDeath = true;
        }
    }
    public void OnGUI()
    {
        if (uiPause)
        {
            if (GUI.Button(new Rect((float)(Screen.width / 2) - 70f, (float)(Screen.height / 2), 200f, 200f), play))
            {
                onPause = false;
            }
            //if (GUI.Button(new Rect((float)(Screen.width / 2) + 50f, (float)(Screen.height / 2), 200f, 200f), home))
            //{
            //    Destroy(LevelLoading.thisLevel);
            //    SceneManager.LoadScene("Map");
            //}
        }
        if (enDeath)
        {
            GUI.Label(new Rect((float)(Screen.width / 2) - 260f, (float)(Screen.height / 2) - 50f, 600f, 600f), win);
        }
        if (heDeath)
        {
            GUI.Label(new Rect((float)(Screen.width / 2) - 290f, (float)(Screen.height / 2) - 50f, 600f, 600f), death);
        }
    }
}
