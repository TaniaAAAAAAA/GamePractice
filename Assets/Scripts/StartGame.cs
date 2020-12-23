using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame : MonoBehaviour
{
    public static int LevelNumber;
    private AudioSource _buttonClickSound;

    private void Awake()
    {
        _buttonClickSound = FindObjectOfType<SoundButtonClick>().GetComponent<AudioSource>();
    }

    public void LoadNeedScene()
    {
        _buttonClickSound.Play();
        SceneManager.LoadScene(LevelNumber);
    }
}
