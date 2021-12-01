using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver) {
            return;
        }
        if(!inPlay) {
            transform.position = paddle.position;    
        }

        if (Input.GetButtonDown("Jump") && !inPlay) { // change this into 3-second timer
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bottom")) {
            Debug.Log("Ball hit the botom of the screen");
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("bricks")) {
            gm.UpdateScore(other.gameObject.GetComponent<BrickScript>().points);
            gm.UpdateBrickLength();
            Destroy(other.gameObject);
        }
    }
}
