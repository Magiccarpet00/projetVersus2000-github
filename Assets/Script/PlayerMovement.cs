using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxMoveSpeed;
    public float currentMoveSpeed;
    public Rigidbody2D rb;
    private Vector2 movement;

    public Animator animator;

    public PlayerHealth playerHealth;
    public PlayerInput playerInput;

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

    public bool frezze;
    

    private void Start()
    {
        currentMoveSpeed = maxMoveSpeed;
    }

    private void Update()
    {
        //Detection
        movement.x = Input.GetAxisRaw(playerInput.horizontalAxeJoypad);
        movement.y = Input.GetAxisRaw(playerInput.verticalAxeJoypad);

        //Buffer
        UpdateInputBuffer();

        //Systeme de frezze
        if(frezze == false)
        {
            currentMoveSpeed = maxMoveSpeed;
        }
        else
        {
            currentMoveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!isBump && playerHealth.dead == false)
        {
            rb.MovePosition(rb.position + movement.normalized * currentMoveSpeed * Time.fixedDeltaTime);
        }
        else if (isBump && playerHealth.dead == false) // On se fait déplacer par le bump sans contrôle du joueur
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

    //public void StopMovement()
    //{
    //    currentMoveSpeed = 0;
    //}

    //public void UnStopMovement()
    //{        
    //    currentMoveSpeed = maxMoveSpeed;              
    //}

    /*
     * Rajoute un élément aléatoire lorsqu'on repousse le joueur
     */
    public IEnumerator Bumping(Vector2 _bumpForce)
    {
        // Calcul aléatoire
        bumpForce.x = _bumpForce.x
                      + Random.Range(-Constants.OFFSET_RANDOM_BUMPING, Constants.OFFSET_RANDOM_BUMPING);

        bumpForce.x = bumpForce.normalized.x * Constants.SPEED_BUMPING;


        bumpForce.y = _bumpForce.y
                      + Random.Range(-Constants.OFFSET_RANDOM_BUMPING, Constants.OFFSET_RANDOM_BUMPING);

        bumpForce.y = bumpForce.normalized.y * Constants.SPEED_BUMPING;


        //que peut faire ce boolean ???
        isBump = true;

        yield return new WaitForSeconds(Constants.TIME_TO_BUMPING);

        isBump = false;
    }
}
