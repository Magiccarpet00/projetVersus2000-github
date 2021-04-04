using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class PlayerAttack : MonoBehaviour
{
    Dictionary<InputBufferDirection, float> direction = new Dictionary<InputBufferDirection, float>();

    public void Awake()
    {
        // on regarde dans quelle dirrection est l'input buffer
        direction[InputBufferDirection.UP] = 90f;
        direction[InputBufferDirection.LEFT] = 180f;
        direction[InputBufferDirection.DOWN] = 270f;
        direction[InputBufferDirection.RIGHT] = 0f;
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
        GameObject epee = Instantiate(epeePrefab, transform.position, Quaternion.Euler(0f,0f,direction[playerMovement.InputBuffer]));
        epee.GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(0.4f);
        Destroy(epee);
        playerMovement.UnStopMovement();
    }
}