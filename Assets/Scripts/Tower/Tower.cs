using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private GameObject _startLevelPanel;
    [SerializeField] private Text _levelNumberText;

    private NoHeartPanel _noHeartPanel;

    public bool IsUnlocked;
    public bool IsComplete;

    private void Awake()
    {
        _noHeartPanel = FindObjectOfType<NoHeartPanel>();
    }
    private void OnMouseDown()
    {
        if (IsUnlocked && !IsComplete)
        {
            if (!HeartController.heartController.IsAllHeartsLost)
            {
                _startLevelPanel.SetActive(true);
                _levelNumberText.text = "Level " + _levelNumber.ToString();
                StartGame.LevelNumber = _levelNumber;
            }
            else
            {
                _noHeartPanel.gameObject.SetActive(true);
            }
        }
    }
}
