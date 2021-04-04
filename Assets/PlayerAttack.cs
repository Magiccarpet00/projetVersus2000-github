using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class PlayerAttack : MonoBehaviour
{
    Dictionary<InputBufferDirection, Vector3> directionOffSet_And_Rotation = new Dictionary<InputBufferDirection, Vector3>();
    
    public void Awake()
    {
        // on regarde dans quelle dirrection est l'input buffer


        directionOffSet_And_Rotation[InputBufferDirection.UP] = new Vector3(0f,Constants.OFFSET_ATTACK,90f);
        directionOffSet_And_Rotation[InputBufferDirection.LEFT] = new Vector3(-Constants.OFFSET_ATTACK, 0f, 180f); 
        directionOffSet_And_Rotation[InputBufferDirection.DOWN] = new Vector3(0f, -Constants.OFFSET_ATTACK, 270f);
        directionOffSet_And_Rotation[InputBufferDirection.RIGHT] = new Vector3(Constants.OFFSET_ATTACK, 0f, 0f);


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


        GameObject epee = Instantiate(epeePrefab,
                                       new Vector2(transform.position.x + directionOffSet_And_Rotation[playerMovement.InputBuffer].x, transform.position.y + directionOffSet_And_Rotation[playerMovement.InputBuffer].y),
                                       Quaternion.Euler(0f, 0f, directionOffSet_And_Rotation[playerMovement.InputBuffer].z));


        epee.GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(0.4f);
        Destroy(epee);
        playerMovement.UnStopMovement();
    }
}