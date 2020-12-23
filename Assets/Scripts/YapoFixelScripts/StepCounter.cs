using UnityEngine;
using UnityEngine.Events;

public class StepCounter : MonoBehaviour
{
    public static StepCounter stepCounter;
    public UnityEvent EndLevelEvents;
    public int StartCount;
    public int Count;
    public double endedStepCount;

    private int _temp = 0;

    private void Awake()
    {
        stepCounter = this;
        StartCount = Count;
    }
    private void Start()
    {
        endedStepCount = Count * 0.3f;
    }
    private void Update()
    {
        if (Count <= 0 && _temp == 0)
        { 
            EndLevelEvents.Invoke();
            _temp++;
        }
    }


}
