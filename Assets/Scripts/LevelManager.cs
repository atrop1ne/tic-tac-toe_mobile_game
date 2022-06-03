using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool isLevelPaused = false;

    private void changeLevelState()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        else
        {
            Time.timeScale = 0f;
        }

        isLevelPaused = !isLevelPaused;
    }

    public void PauseLevel()
    {
        changeLevelState();
        UIManager.instance.ShowPauseMenuScreen();
    }

    public void ResumeLevel()
    {
        changeLevelState();
        UIManager.instance.HidePauseMenuScreen();
    }

    public void EndLevel()
    {
        changeLevelState();
        UIManager.instance.ShowGameOverScreen();
    }

    public void CompleteLevel()
    {
        changeLevelState();
        UIManager.instance.ShowLevelCompleteScreen();
    }

    public void RestartLevel()
    {
        changeLevelState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
}
