using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public float speed = 5f;
    private float initialSpeed = 5f;
    public float speedIncreasesPerPoints = 5;
    public float maxSpeed = 10f;
    public ParticleSystem collisionBomb;
    public AudioSource firework;
    public Player player;
    public Score score;

    private Rigidbody2D ballRigidBody;
    private Vector2 origin;

    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = speed;
        ballRigidBody = GetComponent<Rigidbody2D>();
        origin = transform.position;
    }

    public void startBall()
    {
        float initialAngle = UnityEngine.Random.Range((float)(5 * Math.PI / 4), (float)(7 * Math.PI / 4));
        Vector2 nextVelocity = new Vector2((float)(Math.Cos(initialAngle) * speed), (float)Math.Sin(initialAngle) * speed);
        ballRigidBody.velocity = nextVelocity;
    }

    public void gameOver()
    {
        ballRigidBody.velocity = Vector2.zero;
        ballRigidBody.position = origin;
        speed = initialSpeed;
        GameController.resetGame();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        float vx = ballRigidBody.velocity.x;
        float vy = ballRigidBody.velocity.y;

        if (collision.gameObject.tag == "Player")
        {
            // increment score
            score.increaseScore();

            // play audio
            playAudio();

            // play animation
            playAnimation();

            // find new velocity (speed and angle)
            float ballX = transform.position.x;
            float playerLeft = player.position.x - (player.width / 2);
            float distanceFromPlayer = ballX - playerLeft;
            double angle = (3 * Math.PI / 4) - (distanceFromPlayer / player.width) * (Math.PI / 2);
            Vector2 nextVelocity = new Vector2((float) (Math.Cos(angle) * speed), (float) Math.Sin(angle) * speed);
            ballRigidBody.velocity = nextVelocity;
            Debug.Log("velocity.x = " + nextVelocity.x + ", velocity.y = " + nextVelocity.y + ", angle = " +  angle * 180 / Math.PI);

            // increase speed by 1 for every X points in score
            speed = Math.Min(maxSpeed, initialSpeed + (float) Math.Floor(score.getScore() / speedIncreasesPerPoints));
            Debug.Log("speed = " + speed);
        }
        else if (collision.gameObject.tag == "LeftWall")
        {
            ballRigidBody.velocity = new Vector2(-vx, vy);
        }
        else if (collision.gameObject.tag == "RightWall")
        {
            ballRigidBody.velocity = new Vector2(-vx, vy);
        }
        else if (collision.gameObject.tag == "TopWall")
        {
            ballRigidBody.velocity = new Vector2(vx, -vy);
        }
        else if (collision.gameObject.tag == "BottomWall")
        {
            gameOver();
        }
    }

    private void playAnimation()
    {
        var explosion = Instantiate(collisionBomb, transform.position, Quaternion.identity);
        explosion.Play();
        StartCoroutine(destroyPlayAnimation(explosion));
    }

    IEnumerator destroyPlayAnimation(ParticleSystem explosion)
    {
        yield return new WaitForSeconds(1);
        explosion.Stop();
        Destroy(explosion.gameObject);
    }

    private void playAudio()
    {
        firework.Play();
    }
}
