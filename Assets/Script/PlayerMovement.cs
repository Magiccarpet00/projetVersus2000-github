using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxMoveSpeed;
    public float currentMoveSpeed;
    public Rigidbody2D rb;
    private Vector2 movement;

    public InputBufferDirection InputBuffer = InputBufferDirection.DOWN; //Pcq quand tu commences tu regardes vers le bas
    public enum InputBufferDirection
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    public bool isBump;
    public Vector2 bumpForce;
    

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
        if (!isBump)
        {
            rb.MovePosition(rb.position + movement.normalized * currentMoveSpeed * Time.fixedDeltaTime);
        }
        else if (isBump)
        {
            rb.MovePosition(rb.position + bumpForce * Time.fixedDeltaTime);
        }        
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

    public IEnumerator Bumping(Vector2 _bumpForce)
    {



        bumpForce.x = _bumpForce.normalized.x 
                      + Random.Range(-Constants.OFFSET_RANDOM_BUMPING, Constants.OFFSET_RANDOM_BUMPING) 
                      * Constants.SPEED_BUMPING;

        bumpForce.y = _bumpForce.normalized.y 
                      + Random.Range(-Constants.OFFSET_RANDOM_BUMPING, Constants.OFFSET_RANDOM_BUMPING) 
                      * Constants.SPEED_BUMPING;

        isBump = true;
        yield return new WaitForSeconds(Constants.TIME_TO_BUMPING);
        isBump = false;
    }
}
