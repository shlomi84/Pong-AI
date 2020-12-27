using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool gameInSession = false;

    public Ball ball;
    public Score score;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!gameInSession)
            {
                gameInSession = true;
                ball.startBall();
                score.resetScore();
            }
        }
    }

    public static void resetGame()
    {
        gameInSession = false;
    }
}
