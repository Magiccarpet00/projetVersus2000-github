using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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
            StartCoroutine(Attack()); // C'est bof            
        }        
    }

    public IEnumerator Attack()
    {
         
        




        GameObject epee = Instantiate(epeePrefab, transform.position, Quaternion.identity);
        epee.GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(0.4f);

        playerMovement.UnStopMovement();
    }
}
