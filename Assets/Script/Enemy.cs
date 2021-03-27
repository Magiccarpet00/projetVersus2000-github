using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Varrible Statistique
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public bool dead;
    public bool haveBubble;

    // Varriable patrol
    public float maxSpeed;
    public float speed;
    public Transform[] wayPoints;
    public Transform target;
    private int destinationPoint;
    public bool activated;
    public float timeBeforeMove;

    // Varrible bubble 

    public Room currentRoom;
    
    private void Update()
    {
        if (activated && !dead && timeBeforeMove ==0)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (timeBeforeMove > 0 && activated)
        {
            timeBeforeMove -= Time.fixedDeltaTime;
        }
        else if(timeBeforeMove <= 0 && activated)
        {
            timeBeforeMove = 0;
        }
    }

    public void Patrol()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //Si l'ennemi est quasiment arriver à destination
        if (Vector3.Distance(transform.position, target.position) < Constants.DESTINATION_PROCHE)
        {
            destinationPoint = (destinationPoint + 1) % wayPoints.Length;
            target = wayPoints[destinationPoint];
        }
    }

    //[codeReview]
    public void Die()
    {
        dead = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        OpenDoorWhenRoomCleared(); // non !!! ce n'est pas à l'ennemi d'ouvrir les portes lui il meurt et annonce qu'il meurt c'est tout

        //GameManager.instance.whereIsPlayer().GetComponent<Room>().notifyDeath(); // Baby version
        currentRoom.notifyDeath(); // Adult version
    }

    //[codeReview]
    /*
     * A voir pour mettre dans la classe Room. Pour moi l'ennemy doit informer la room qu'il y a des morts, mais pas faire
     * l'ouverture à sa place 
     */
    public void OpenDoorWhenRoomCleared() // Cette methode prend plein d'info de partout, je sais pas si sa place est ici (très bonne question)
    {
        GameObject currentRoom = GameManager.instance.whereIsPlayer();
        bool allEnemiesInRoomAreDead = currentRoom.GetComponent<Room>().patternInThisRoom.GetComponent<PatternEnemy>().PatternEnemyCleaned();
        if (allEnemiesInRoomAreDead == true)
        {
            currentRoom.GetComponent<Room>().roomFinnished = true;
            currentRoom.GetComponent<Room>().OpenDoor();
        }
    }


    // Provisoire
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!dead)
            {
                Die();
            }            
        }
    }



}
