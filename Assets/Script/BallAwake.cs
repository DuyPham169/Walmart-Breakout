using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAwake : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rb.velocity = Vector2.down * moveSpeed;
    }
}
