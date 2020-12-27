using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    // Player Slider
    private Rigidbody2D playerRigidBody;
    public float width { get; set; }
    public Vector2 position { get; set; }

    private bool hitLeftWall = false;
    private bool hitRightWall = false;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        width = GetComponent<BoxCollider2D>().size.x;
        position = transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (hitLeftWall)
                return;
            playerRigidBody.velocity = new Vector2(speed * -1, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (hitRightWall)
                return;
            playerRigidBody.velocity = new Vector2(speed, 0);
        }
        else
            stopPlayer();
        position = transform.position;
    }

    private void stopPlayer()
    {
        playerRigidBody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        if (collision.gameObject.tag == "LeftWall")
        {
            hitLeftWall = true;
            stopPlayer();
        }
        else if (collision.gameObject.tag == "RightWall")
        {
            hitRightWall = true;
            stopPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftWall")
        {
            hitLeftWall = false;
        }
        else if (collision.gameObject.tag == "RightWall")
        {
            hitRightWall = false;
        }
    }
}