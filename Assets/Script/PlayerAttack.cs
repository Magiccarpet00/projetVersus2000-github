using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class PlayerAttack : MonoBehaviour //C'est plus vraiment player attaque c'est player Ability  
{
    public bool isAttacking;
    public bool isBufferingAttack;
    public bool isDashing;

    Dictionary<InputBufferDirection, InfoAttack> directionOffSet_And_Rotation = new Dictionary<InputBufferDirection, InfoAttack>();

    public Animator animator;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerInput playerInput;
    public PlayerInventory playerInventory;
    public PlayerCharacter playerCharacter;

    public void Awake()
    {
        // on regarde dans quelle dirrection est l'input buffer
        directionOffSet_And_Rotation[InputBufferDirection.UP] = new InfoAttack(new Vector2(0f, Constants.OFFSET_ATTACK), 90f,Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.LEFT] = new InfoAttack(new Vector2(-Constants.OFFSET_ATTACK,0f), 180f, Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.DOWN] = new InfoAttack(new Vector2(0f, -Constants.OFFSET_ATTACK), 270f, Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.RIGHT] = new InfoAttack(new Vector2(Constants.OFFSET_ATTACK, 0f), 0f, Constants.SECOND_ATTACK_CD);        
    }    
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        playerInput = GetComponent<PlayerInput>();
        playerInventory = GetComponent<PlayerInventory>();
        animator = GetComponentInChildren<Animator>();
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    public struct InfoAttack
    {
        public InfoAttack(Vector2 offsetAttack, float rotatiisAttacking, float delayAttack)
        {
            OffsetAttack = offsetAttack;
            RotatiisAttacking = Quaternion.Euler(0f, 0f, rotatiisAttacking);
            DelayAttack = delayAttack;
        }

        public Vector2 OffsetAttack { get; set; }
        public Quaternion RotatiisAttacking { get; set; }
        public float DelayAttack { get; set; }
    }
    
    void Update()
    {   // Bouton A enfoncer
        if (Input.GetButton(playerInput.button0) 
            && !isAttacking 
            && !playerHealth.isInvincible 
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms)
        {
            if(playerCharacter.character == Character.BLUE)
            {
                BluePreCloseAttack();
            }

            else if(playerCharacter.character == Character.RED)
            {

            }

            else if (playerCharacter.character == Character.GREEN)
            {

            }
        }

        // Bouton A relacher
        else if (Input.GetButtonUp(playerInput.button0)
                && !isAttacking 
                && !playerHealth.isInvincible 
                && !playerHealth.dead
                && !playerMovement.isBetweenRooms)
        {
             if(playerCharacter.character == Character.BLUE)
             {
                    StartCoroutine(BlueCloseAttack());
             }

             else if(playerCharacter.character == Character.RED)
             {

             }

             else if (playerCharacter.character == Character.GREEN)
             {

             }
        }

        // Bouton B enfoncer
        if (Input.GetButtonDown(playerInput.button1)
            && !isAttacking
            && !playerHealth.isInvincible
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms
            && playerInventory.munitionRangeAttack > 0
            )
        {
            if (playerCharacter.character == Character.BLUE)
            {
                BlueRangeAttaque();
            }

            else if (playerCharacter.character == Character.RED)
            {

            }

            else if (playerCharacter.character == Character.GREEN)
            {

            }
        }

        // Bouton X Enfoncer 
        if (Input.GetButtonDown(playerInput.button2)
            && !isAttacking
            && !playerHealth.isInvincible
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms
            && !isDashing)
        {
            if (playerCharacter.character == Character.BLUE)
            {
                StartCoroutine(BlueDash());
            }

            else if (playerCharacter.character == Character.RED)
            {

            }

            else if (playerCharacter.character == Character.GREEN)
            {

            }
        }
    }

    // BLUE COMPETENCE
    public void BluePreCloseAttack()
    {
        animator.SetBool("Button_Down", true);

        isBufferingAttack = true;
        playerMovement.checkSwitchBoxMove("isBufferingAttack", isBufferingAttack);
    }
    public IEnumerator BlueCloseAttack()
    {
        isBufferingAttack = false;
        playerMovement.checkSwitchBoxMove("isBufferingAttack", isBufferingAttack);
        //[Annimation]
        animator.SetBool("Button_Down", false);

        //Lance l'attaque
        isAttacking = true;
        playerMovement.checkSwitchBoxMove("isAttacking", isAttacking);

        InfoAttack infoAttack = directionOffSet_And_Rotation[playerMovement.InputBuffer];
        GameObject closeAttack = Instantiate(playerCharacter.closeAttackPrefab,
                                             transform.position + transform.TransformDirection(infoAttack.OffsetAttack),
                                             infoAttack.RotatiisAttacking);

        closeAttack.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(infoAttack.DelayAttack);

        isAttacking = false;
        playerMovement.checkSwitchBoxMove("isAttacking", isAttacking);
    }    
    public void BlueRangeAttaque()
    {
        animator.SetTrigger("flip");
        Vector2 posRangeAttaque = new Vector2(transform.position.x, transform.position.y + 0.5f);
        GameObject rangeAttack = Instantiate(playerCharacter.rangeAttackPrefab, posRangeAttaque, Quaternion.identity);

        float speedTime = 1f;
        float speedStrenght = 0.6f;
        StartCoroutine(SpeedUp(speedTime, speedStrenght)); // [CodeReview] Je sais pas ou mettre les caracteristique des competence je trouve que dans constante c'est bizzare

        playerInventory.munitionRangeAttack--;
        playerInventory.UpdateUI();
    }
    public IEnumerator BlueDash()
    {
        float dashTime = 0.2f;
        float dashStrenght = 4f;

        isDashing = true;
        animator.SetTrigger("dash");
        StartCoroutine(SpeedUp(dashStrenght, dashTime));
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }

    // RED COMPETENCE


    // GREEN COMPETENCE


    public IEnumerator SpeedUp(float buffSpeed, float buffTime)
    {
        playerMovement.maxMoveSpeed += buffSpeed;
        yield return new WaitForSeconds(buffTime/2);
        playerMovement.maxMoveSpeed -= buffSpeed/2;
        yield return new WaitForSeconds(buffTime/2);
        playerMovement.maxMoveSpeed -= buffSpeed/2;
    }

}