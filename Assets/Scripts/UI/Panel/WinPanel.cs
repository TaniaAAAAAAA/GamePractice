﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Text _titleText;
    [SerializeField] private Text _winLoseText;
    [SerializeField] private Text _rewardText;

    private void Start()
    {
        _titleText.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
        _winLoseText.text = "You win!";
    }
    private void Update()
    {
        if (GameManager.gameManager.IsWin)
        {
            _winLoseText.text = "You win!";
            _rewardText.text = GameManager.gameManager.Reward.ToString();
        }
        else
        {
            _winLoseText.text = "You lose!";
            _rewardText.text = GameManager.gameManager.Reward.ToString();
        }
    }

    public void BackToLastScene()
    {
        SceneManager.LoadScene("Main");
    }
}
