using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject level;

    static public GameObject getLevel;
    static public int onGame = 0;
    public void GetLevel(GameObject level)
    {
        getLevel = level;
        onGame = 1;
    }
}
