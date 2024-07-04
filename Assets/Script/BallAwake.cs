using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAwake : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float barHalfLength;
    [SerializeField] private float vectorScale;

    private void Awake()
    {
        rb.velocity = Vector2.down * moveSpeed;
    }

    private void Update()
    {
        increaseBallSpeed();
    }

    private void increaseBallSpeed() => moveSpeed += Time.deltaTime / 10;

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
    }
}
