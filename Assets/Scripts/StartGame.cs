using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame : MonoBehaviour
{
    public static int LevelNumber;

    public void LoadNeedScene()
    {
        SceneManager.LoadScene(LevelNumber);
    }
}
