using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    //Creating and updating players lives (array of hearts) through the gameplay
    [SerializeField] private Image[] _lives;
    public int numOfLives;
    private const int MAX_LIVES = 3;

    public Sprite activeHeart;
    public Sprite inactiveHeart;

    private void Start()
    {
        numOfLives = MAX_LIVES;
    }

    private void Update()
    {
        DisplayLives(numOfLives);
    }

    public int GetLives() { return numOfLives; }
    public void DisplayLives(int numOfLives)
    {
        foreach (Image heart in this._lives)
        {
            heart.sprite = inactiveHeart;
        }
        
        for (int i = 0; i < numOfLives; i++)
        {
            _lives[i].sprite = activeHeart;
        }
    }

    //if lives are not full when a life bonus is picked up, number of lives is being increased => life is added
    //else - additional points are added to score
    public void AddLife()
    {
        if (numOfLives < MAX_LIVES)
            numOfLives++;
        else
            FindObjectOfType<GameStats>().AddToScore(500);
    }
}
