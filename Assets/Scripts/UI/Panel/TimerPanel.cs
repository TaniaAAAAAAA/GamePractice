using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerPanel : MonoBehaviour
{
    private Text _levelNumberText;
    private Text _timeLeftText;

    private void Start()
    {
        _levelNumberText = FindObjectOfType<LevelNumberText>().GetComponent<Text>();
        _levelNumberText.text = SceneManager.GetActiveScene().buildIndex.ToString();
        _timeLeftText = FindObjectOfType<TimeText>().GetComponent<Text>();
    }
    private void FixedUpdate()
    {
        if(Timer.timer.CurrentTime >= 0)
        _timeLeftText.text = Timer.timer.CurrentTime.ToString();
    }
}
