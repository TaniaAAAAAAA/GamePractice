using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private DateTime _leaveGameTime;
    private DateTime _currentGameTime;

    private int _isQuit = 0;
    private float tempSeconds = 0;

    public static float MinutesPassed;

    private void Awake()
    {
        long temp = Convert.ToInt64(PlayerPrefs.GetString("LeaveTime"));
        _currentGameTime = DateTime.Now;
        _leaveGameTime = DateTime.FromBinary(temp);
    }
    private void Start()
    {
        _isQuit = PlayerPrefs.GetInt("IsQuit");
        if (_isQuit == 1)
        {
            _isQuit = 0;
            if (HeartController.heartController.LostHeartsCount == 0) PlayerPrefs.SetFloat("MinutesPassed", 0);
            MinutesPassed = PlayerPrefs.GetFloat("MinutesPassed");
            MinutesPassed += LeaveTimeCalculation(_currentGameTime, _leaveGameTime);
            PlayerPrefs.SetInt("IsQuit", _isQuit);
            DontDestroyOnLoad(this);
        }
    }
    private void FixedUpdate()
    {
        if (HeartController.heartController.LostHeartsCount > 0)
            OnGameTimeCalculation();
        else PlayerPrefs.SetFloat("MinutesPassed", 0);
    }
  
    private void OnApplicationQuit()
    {
        _leaveGameTime = DateTime.Now;
        _isQuit = 1;
        PlayerPrefs.SetString("LeaveTime", _leaveGameTime.ToBinary().ToString());
        PlayerPrefs.SetFloat("MinutesPassed", MinutesPassed);
        PlayerPrefs.SetInt("IsQuit",_isQuit);
        Destroy(this);
    }  
    private float LeaveTimeCalculation(DateTime currentTime, DateTime leaveTime)
    {
        var timeCount = currentTime - leaveTime;
        var hour = timeCount.Hours;
        var minutes = timeCount.Minutes;
        float minutesPassed = 0;
        for (int i = 0; i < hour; i++)
        {
            minutesPassed += 60;
        }
        minutesPassed += minutes;
        return minutesPassed;
    }
    private void OnGameTimeCalculation()
    {
        tempSeconds += Time.deltaTime;
        if (tempSeconds >= 60)
        {
            MinutesPassed += 1;
            tempSeconds = 0;
        }
    }
}
