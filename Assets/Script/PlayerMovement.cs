using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxMoveSpeed;
    public float currentMoveSpeed;
    public Rigidbody2D rb;
    private Vector2 movement;

    
    public enum InputBufferDirection
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }
    public InputBufferDirection InputBuffer = InputBufferDirection.DOWN; //Pcq quand tu commences tu regardes vers le bas

    private void Start()
    {
        currentMoveSpeed = maxMoveSpeed;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateInputBuffer();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * currentMoveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateInputBuffer()
    {
        if (movement.x >= Constants.RADIUS_JOYSTICK)
        {
            InputBuffer = InputBufferDirection.RIGHT;
        }

        if (movement.x <= -Constants.RADIUS_JOYSTICK)
        {
            InputBuffer = InputBufferDirection.LEFT;
        }

        if (movement.y >= Constants.RADIUS_JOYSTICK)
        {
            InputBuffer = InputBufferDirection.UP;
        }

        if (movement.y <= -Constants.RADIUS_JOYSTICK)
        {
            InputBuffer = InputBufferDirection.DOWN;
        }
    }

    public void StopMovement()
    {
        currentMoveSpeed = 0;
    }

    public void UnStopMovement()
    {
        currentMoveSpeed = maxMoveSpeed;
    }
}
