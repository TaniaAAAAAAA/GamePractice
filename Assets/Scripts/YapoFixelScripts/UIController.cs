using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController uiController;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _analizeButton;

    private AudioSource _buttonClickSound;

    private void Start()
    {
        _buttonClickSound = FindObjectOfType<SoundButtonClick>().GetComponent<AudioSource>();
        uiController = this;
        _gameManager.GetColors();
    }

    public void ButtonGetColorsOnClick()
    {
        _gameManager.GetColors();
        _analizeButton.gameObject.SetActive(true);
    }
    public void ButtonAnalyzeOnClick()
    {
        _buttonClickSound.Play();
        _gameManager.Analyze();
    }
    public void RestartButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void HomeButtonOnClick()
    {
        SceneManager.LoadScene("Main");
    }

}
