using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    private int score;

    void Start()
    {
        score = 0;
    }

    private void Update()
    {
        if (GameController.gameInSession)
        {
            setScoreText($"Score: {score}");
        }
        else
        {
            setScoreText($"Score: {score}\nPress Space To Play");
        }
    }

    public void setScoreText(string text)
    {
        scoreText.text = text;
    }

    public void increaseScore()
    {
        score++;
    }

    public void resetScore()
    {
        score = 0;
    }

    public int getScore()
    {
        return score;
    }
}
