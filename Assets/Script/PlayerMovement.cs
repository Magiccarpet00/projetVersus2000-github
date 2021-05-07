using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxMoveSpeed;
    public float currentMoveSpeed;
    private Vector2 movement;

    public Animator animator;
    public Rigidbody2D rb;
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
    public bool isBetweenRooms;

    public Vector2 directionAutoWalk;

    // Pour le derappage 
    private bool onSlide;
    public Vector2 destinationSlide;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerInput = GetComponent<PlayerInput>();
        playerAttack = GetComponent<PlayerAttack>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        currentMoveSpeed = maxMoveSpeed;
        canMove = true;
        switchBoxMove = new Dictionary<string, bool>();
        switchBoxMove["isBumped"] = false;
        switchBoxMove["betweenRooms"] = false;

        /*
         * [CODEREVIEW] Il faut utiliser les ENUM globales et pas celles locales
         */
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

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.magnitude > Constants.RADIUS_JOYSTICK)
        {
            onSlide = false;
        }
        else
        {
            onSlide = true;
        }

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
        // Félix à "tout compris ;)         
        if (!playerHealth.dead)
        {
            if(!isBumped)
            {
                if (playerAttack.isAttacking) // Pendant l'attaque, ça glisse, attention secousse !
                {                  
                    // PAS SUR DE CETTE MECANIQUE
                    //rb.MovePosition(rb.position + (directionVector[InputBuffer]) * Time.fixedDeltaTime);
                }
                else if (playerAttack.isBufferingAttack)
                {
                    Slide();
                }
                else if (isBetweenRooms) // Quand on est dans l'entre deux room
                {
                    rb.MovePosition(rb.position + directionAutoWalk * maxMoveSpeed * Time.fixedDeltaTime);
                    
                }
                else // Deplacement normale
                {
                    // truc trouver sur le web...
                    //rb.velocity = new Vector2(Mathf.Lerp(0, movement.x * currentMoveSpeed, 0.8f),
                    //                            Mathf.Lerp(0, movement.y * currentMoveSpeed, 0.8f));
                    if (!onSlide)
                    {
                        rb.MovePosition(rb.position + movement.normalized * currentMoveSpeed * Time.fixedDeltaTime);
                        destinationSlide = new Vector2(transform.position.x + (movement.x)/Constants.SLIDE_DIVISION,
                                                       transform.position.y + (movement.y)/Constants.SLIDE_DIVISION);
                        
                    }
                    else
                    {
                        Slide();
                    }
                }
            }
            
            else if (isBumped) // On se fait déplacer par le bump sans contrôle du joueur
            {
                rb.MovePosition(rb.position + bumpForce * Time.fixedDeltaTime);
            }
        }
    }

    public void Slide()
    {        
        transform.position = new Vector2(Mathf.Lerp(transform.position.x, destinationSlide.x, Constants.SLIDE_MOVEMENT),
                                         Mathf.Lerp(transform.position.y, destinationSlide.y, Constants.SLIDE_MOVEMENT));        
    }

    private void UpdateInputBuffer()
    {
        if(playerAttack.isAttacking == false)
        {
            if (movement.x >= Constants.RADIUS_JOYSTICK)
            {
                InputBuffer = InputBufferDirection.RIGHT;
                animator.SetFloat("Buffer_Horizontal", 1f);
                animator.SetFloat("Buffer_Vertical", 0f);
            }

            if (movement.x <= -Constants.RADIUS_JOYSTICK)
            {
                InputBuffer = InputBufferDirection.LEFT;
                animator.SetFloat("Buffer_Horizontal", -1f);
                animator.SetFloat("Buffer_Vertical", 0f);
            }

            if (movement.y >= Constants.RADIUS_JOYSTICK)
            {
                InputBuffer = InputBufferDirection.UP;
                animator.SetFloat("Buffer_Horizontal", 0f);
                animator.SetFloat("Buffer_Vertical", 1f);
            }

            if (movement.y <= -Constants.RADIUS_JOYSTICK)
            {
                InputBuffer = InputBufferDirection.DOWN;
                animator.SetFloat("Buffer_Horizontal", 0f);
                animator.SetFloat("Buffer_Vertical", -1f);
            }
        }        
    }

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
        destinationSlide = transform.position;
    }

    /*
     * Faudra remplacer la clé par une enumération
     */
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

    public void TapisRoulant(Room roomOrigine, Room roomDestination)
    {
        isBetweenRooms = true;
        checkSwitchBoxMove("betweenRooms", isBetweenRooms);

       

        directionAutoWalk = new Vector2(roomDestination.transform.position.x - roomOrigine.transform.position.x,
                                        roomDestination.transform.position.y - roomOrigine.transform.position.y);

        directionAutoWalk = directionAutoWalk.normalized;
    }

}
