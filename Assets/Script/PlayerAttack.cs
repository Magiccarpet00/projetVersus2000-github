﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class PlayerAttack : MonoBehaviour
{
    public bool isAttacking;
    public bool isBufferingAttack;
    public bool isDashing;

    Dictionary<InputBufferDirection, InfoAttack> directionOffSet_And_Rotation = new Dictionary<InputBufferDirection, InfoAttack>();

    public GameObject rangeAttackPrefab;
    public GameObject epeePrefab;

    public Animator animator;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerInput playerInput;
    public PlayerInventory playerInventory;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        playerInput = GetComponent<PlayerInput>();
        playerInventory = GetComponent<PlayerInventory>();
        animator = GetComponentInChildren<Animator>();
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

    public void Awake()
    {
        // on regarde dans quelle dirrection est l'input buffer
        directionOffSet_And_Rotation[InputBufferDirection.UP] = new InfoAttack(new Vector2(0f, Constants.OFFSET_ATTACK), 90f,Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.LEFT] = new InfoAttack(new Vector2(-Constants.OFFSET_ATTACK,0f), 180f, Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.DOWN] = new InfoAttack(new Vector2(0f, -Constants.OFFSET_ATTACK), 270f, Constants.SECOND_ATTACK_CD);
        directionOffSet_And_Rotation[InputBufferDirection.RIGHT] = new InfoAttack(new Vector2(Constants.OFFSET_ATTACK, 0f), 0f, Constants.SECOND_ATTACK_CD);        
    }    
    
    void Update()
    {   //ATTACK CAC
        if (Input.GetButton(playerInput.button0) 
            && !isAttacking 
            && !playerHealth.isInvincible 
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms)
        {
            //[Annimation]
            animator.SetBool("Button_Down", true);

            isBufferingAttack = true;
            playerMovement.checkSwitchBoxMove("isBufferingAttack", isBufferingAttack);
        }

        else if (Input.GetButtonUp(playerInput.button0)
                && !isAttacking 
                && !playerHealth.isInvincible 
                && !playerHealth.dead
                && !playerMovement.isBetweenRooms) // isInvincible : c'est quand on se fait touché
        {
            isBufferingAttack = false;
            playerMovement.checkSwitchBoxMove("isBufferingAttack", isBufferingAttack);
            //[Annimation]
            animator.SetBool("Button_Down", false);

            //Lance l'attaque
            isAttacking = true;
            playerMovement.checkSwitchBoxMove("isAttacking", isAttacking);

            StartCoroutine(Attack());          
        }

        // RANGE ATTACK
        if (Input.GetButtonDown(playerInput.button1)
            && !isAttacking
            && !playerHealth.isInvincible
            && !playerHealth.dead
            && !playerMovement.isBetweenRooms
            && playerInventory.munitionRangeAttack > 0
            )
        {
            Debug.Log("buuton 1");
            RangeAttaque();
            
        }

        //if (Input.GetButtonDown(playerInput.button2)
        //    && !isAttacking
        //    && !playerHealth.isInvincible
        //    && !playerHealth.dead
        //    && !playerMovement.isBetweenRooms
        //    && !isDashing)
        //{
        //    StartCoroutine(Dash());
        //}
    }

    public IEnumerator Attack()
    {
        InfoAttack infoAttack = directionOffSet_And_Rotation[playerMovement.InputBuffer];
        GameObject epee = Instantiate(epeePrefab, transform.position + transform.TransformDirection(infoAttack.OffsetAttack), infoAttack.RotatiisAttacking);
        //epee.GetComponent<Sword>().player = this.gameObject; // Pour dire à qui appartient cette épee;

        epee.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(infoAttack.DelayAttack);
        
        //Destroy(epee);

        isAttacking = false;
        playerMovement.checkSwitchBoxMove("isAttacking", isAttacking);
        
    }

    
    public void RangeAttaque()
    {
        animator.SetTrigger("flip");
        Vector2 posRangeAttaque = new Vector2(transform.position.x, transform.position.y + 0.5f);
        GameObject rangeAttack = Instantiate(rangeAttackPrefab, posRangeAttaque, Quaternion.identity);

        float speedTime = 1f;
        float speedStrenght = 0.6f;
        StartCoroutine(SpeedUp(speedTime, speedStrenght)); // [CodeReview] Je sais pas ou mettre les caracteristique des competence je trouve que dans constante c'est bizzare

        playerInventory.munitionRangeAttack--;
        playerInventory.UpdateUI();
    }
    public IEnumerator SpeedUp(float buffSpeed, float buffTime)
    {
        playerMovement.maxMoveSpeed += buffSpeed;
        yield return new WaitForSeconds(buffTime/2);
        playerMovement.maxMoveSpeed -= buffSpeed/2;
        yield return new WaitForSeconds(buffTime/2);
        playerMovement.maxMoveSpeed -= buffSpeed/2;
    }

    public IEnumerator Dash()
    {
        float dashTime = 0.5f;
        float dashStrenght = 3f;

        isDashing = true;
        animator.SetTrigger("flip");
        StartCoroutine(SpeedUp(dashStrenght, dashTime));
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }



}