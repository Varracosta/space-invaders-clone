using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    //Displaying players score
    private GameStats _gameStats;
    private Text _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<Text>();
        _gameStats = FindObjectOfType<GameStats>();
    }

    private void Update()
    {
        _scoreText.text = _gameStats.GetScore().ToString();
    }
}
