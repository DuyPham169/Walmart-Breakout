using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAwake : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float barHalfLength;
    [SerializeField] private float vectorScale;
    [SerializeField] private int speedIncreaseFactor;

    private Vector3 originalPos;
    private float originalMovespeed;
    private float horizontalTimer;

    private void Start()
    {
        originalPos = transform.position;
        originalMovespeed = moveSpeed;
    }

    private void Update()
    {
        HandleHorizontalStuck();
        increaseBallSpeed();
    }

    public void StartBall() => rb.velocity = Vector2.down * moveSpeed;

    private void increaseBallSpeed() => moveSpeed += Time.deltaTime / speedIncreaseFactor;

    private void HandleHorizontalStuck()
    {
        if (rb.velocity.y == 0)
        {
            horizontalTimer += Time.deltaTime;

            if (horizontalTimer > 10.0f)
            {
                LevelTransition.instance.livesLeft += 1;
                horizontalTimer = 0;
                BallReset();
            }
        }
        else
        {
            horizontalTimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            float xBar = collision.gameObject.transform.position.x;
            float xBall = transform.position.x;

            // (xBall - xBar) / barHalfLength to get the where the ball landed
            // in proportion to the bar length
            //
            // Vector scale increases => bounce angle increase
            float xVector = ((xBall - xBar) / barHalfLength) * vectorScale * moveSpeed;
            rb.velocity = new Vector2(xVector, moveSpeed);
        }

        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallReset();
    }

    private void BallReset()
    {
        GameManager.instance.DeductLives();

        rb.velocity = Vector2.zero;
        transform.position = originalPos;
        moveSpeed = originalMovespeed;
    }
}
