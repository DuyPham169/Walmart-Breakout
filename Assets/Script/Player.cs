using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    private float xInput;

    // Update is called once per frame
    private void Update()
    {
        handleMovement();
    }

    private void handleMovement()
    {
        /* This moves according to keyboard
         * 
        // xInput = Input.GetAxisRaw("Horizontal");
        // xInput = Input.GetAxis("Mouse X");

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);*/


        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.MovePosition(new Vector2(mousePos.x, transform.position.y));

        // line below moves through colliders, line above + set to continuous doesn't
        // transform.position = new Vector2(mousePos.x, transform.position.y);
    }
}
