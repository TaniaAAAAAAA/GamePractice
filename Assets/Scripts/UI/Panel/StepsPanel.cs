using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StepsPanel : MonoBehaviour
{
    private Text _levelNumberText;

    public static StepsPanel stepsPanel;
    public Text StepsCountText;

    private void Awake()
    {
        stepsPanel = this;
    }
    private void Start()
    {
        StepsCountText.text = StepCounter.stepCounter.Count.ToString();
        _levelNumberText = FindObjectOfType<LevelNumberText>().GetComponent<Text>();
        _levelNumberText.text = SceneManager.GetActiveScene().buildIndex.ToString();
    }
}
