using System;
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
    public PlayerAttack playerAttack;

    public Dictionary<string, bool> switchBoxMove;
    public Dictionary<InputBufferDirection, Vector2> directionVector = new Dictionary<InputBufferDirection, Vector2>();

    public InputBufferDirection InputBuffer = InputBufferDirection.DOWN; //Pcq quand tu commences tu regardes vers le bas
    public enum InputBufferDirection
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    public bool isBumped;
    
    public Vector2 bumpForce;

    public bool canMove;
    public bool isStunned;

    private void Start()
    {
        currentMoveSpeed = maxMoveSpeed;
        canMove = true;
        switchBoxMove = new Dictionary<string, bool>();
        switchBoxMove["isBumped"] = false;

        //Dictornaire des directions pour l'attque 
        directionVector.Add(InputBufferDirection.DOWN,new Vector2(0f, -Constants.VECTOR_DIRECTION_ATTACK));
        directionVector.Add(InputBufferDirection.LEFT, new Vector2(-Constants.VECTOR_DIRECTION_ATTACK,0f));
        directionVector.Add(InputBufferDirection.RIGHT, new Vector2(Constants.VECTOR_DIRECTION_ATTACK, 0f));
        directionVector.Add(InputBufferDirection.UP, new Vector2(0f, Constants.VECTOR_DIRECTION_ATTACK));


    }

    private void Update()
    {
        //Detection
        movement.x = Input.GetAxisRaw(playerInput.horizontalAxeJoypad);
        movement.y = Input.GetAxisRaw(playerInput.verticalAxeJoypad);

        //Buffer
        UpdateInputBuffer();

        //Systeme de frezze
        if(canMove == true)
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
        if (!isBumped && playerHealth.dead == false && playerAttack.isAttacking == true)
        {
            rb.MovePosition(rb.position + (directionVector[InputBuffer]) * Time.fixedDeltaTime);
        }
        else if (!isBumped && playerHealth.dead == false)
        {
            rb.MovePosition(rb.position + movement.normalized * currentMoveSpeed * Time.fixedDeltaTime);
        }
        else if (isBumped && playerHealth.dead == false) // On se fait déplacer par le bump sans contrôle du joueur
        {
            rb.MovePosition(rb.position + bumpForce * Time.fixedDeltaTime);
        }
    }

    private void UpdateInputBuffer()
    {
        if(playerAttack.isAttacking == false)
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
                      + UnityEngine.Random.Range(-Constants.OFFSET_RANDOM_BUMPING, Constants.OFFSET_RANDOM_BUMPING);

        bumpForce.x = bumpForce.normalized.x * Constants.SPEED_BUMPING;


        bumpForce.y = _bumpForce.y
                      + UnityEngine.Random.Range(-Constants.OFFSET_RANDOM_BUMPING, Constants.OFFSET_RANDOM_BUMPING);

        bumpForce.y = bumpForce.normalized.y * Constants.SPEED_BUMPING;


        //que peut faire ce boolean ???
        isBumped = true;
        checkSwitchBoxMove("isBumped", isBumped);

        yield return new WaitForSeconds(Constants.TIME_TO_BUMPING);

        isBumped = false;
        checkSwitchBoxMove("isBumped", isBumped);
    }

    public void checkSwitchBoxMove(string key, bool value)
    {
        switchBoxMove[key] = value;
        /*
         * Je regarde tous mes interrupteurs
         * Si un seul est vrai je n'ai pas le droit de me déplacer
         * Donc là j'appelle Containsvalue sur mon dictionnaire pour voir si il n'y a ne serait ce qu'une
         * seule valeur qui est à vrai, dans ce cas je n'ai pas le droit de me déplacer. Sinon tout va bien
         */
        canMove = switchBoxMove.ContainsValue(true) ? false : true; // so easy... >;-) oh yeah

        if (switchBoxMove.ContainsValue(true))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }
}
