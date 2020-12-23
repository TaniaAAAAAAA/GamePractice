using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static Timer timer;
    public UnityEvent EndLevelEvents;
    public int StartTime;
    public int CurrentTime;
    public double endedTime;

    private int _temp = 0;
    private double _gameTime;
    private TimerPanel _timerPanel;

    private void Awake()
    {
        timer = this;
        _timerPanel = FindObjectOfType<TimerPanel>();
    }
    private void Start()
    {
        StartTime = CurrentTime;
        endedTime = CurrentTime * 0.3f;
    }
    private void FixedUpdate()
    {
        _gameTime += 1 * Time.deltaTime;
        if(_gameTime>=1 && _temp == 0 && !GameManager.gameManager.IsWin)
        {
            if (CurrentTime <= 10 && CurrentTime!=0) _timerPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>().SetTrigger("Ended");
            CurrentTime -= 1;
            _gameTime = 0;
        }

        if (CurrentTime <= 0 && _temp == 0)
        {
            EndLevelEvents.Invoke();
            _temp++;
        }
    }
}
