﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class PlayerAttack : MonoBehaviour
{
    Dictionary<InputBufferDirection, InfoAttack> directionOffSet_And_Rotation = new Dictionary<InputBufferDirection, InfoAttack>();
    public struct InfoAttack
    {
        public InfoAttack(Vector2 offsetAttack, float rotationAttack, float delayAttack)
        {
            OffsetAttack = offsetAttack;
            RotationAttack = Quaternion.Euler(0f, 0f, rotationAttack);
            DelayAttack = delayAttack;
        }

        public Vector2 OffsetAttack { get; set; }
        public Quaternion RotationAttack { get; set; }
        public float DelayAttack { get; set; }
    }

    public void Awake()
    {
        // on regarde dans quelle dirrection est l'input buffer
        directionOffSet_And_Rotation[InputBufferDirection.UP] = new InfoAttack(new Vector2(0f, Constants.OFFSET_ATTACK), 90f,0.5f);
        directionOffSet_And_Rotation[InputBufferDirection.LEFT] = new InfoAttack(new Vector2(-Constants.OFFSET_ATTACK,0f), 180f, 0.5f);
        directionOffSet_And_Rotation[InputBufferDirection.DOWN] = new InfoAttack(new Vector2(0f, -Constants.OFFSET_ATTACK), 270f, 0.5f);
        directionOffSet_And_Rotation[InputBufferDirection.RIGHT] = new InfoAttack(new Vector2(Constants.OFFSET_ATTACK, 0f), 0f, 0.5f);

    }
    public GameObject epeePrefab;
    public PlayerMovement playerMovement;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Stop les mouvements
            playerMovement.StopMovement();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            //lance l'attaque
            StartCoroutine(Attack()); // C'est bof d'après félix
        }        
    }

    public IEnumerator Attack()
    {

        InfoAttack infoAttack = directionOffSet_And_Rotation[playerMovement.InputBuffer];
        GameObject epee = Instantiate(epeePrefab, transform.position + transform.TransformDirection(infoAttack.OffsetAttack), infoAttack.RotationAttack);


        //GameObject epee = Instantiate(epeePrefab,
        //                               new Vector2(transform.position.x + directionOffSet_And_Rotation[playerMovement.InputBuffer].x, transform.position.y + directionOffSet_And_Rotation[playerMovement.InputBuffer].y),
        //                               Quaternion.Euler(0f, 0f, directionOffSet_And_Rotation[playerMovement.InputBuffer].z));

        epee.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(infoAttack.DelayAttack);
        
        Destroy(epee);
        playerMovement.UnStopMovement();
    }
}