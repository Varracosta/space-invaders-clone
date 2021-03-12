using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    //collecting and updating player's score
    private int _score;
    private void Awake()
    {
        if(FindObjectsOfType<GameStats>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() { return _score; }
    public void AddToScore(int scoreValue)
    {
        _score += scoreValue;
    }
}
