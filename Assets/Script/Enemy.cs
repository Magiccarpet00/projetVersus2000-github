﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Varrible Statistique
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public bool dead;

    // Varriable patrol
    public float maxSpeed;
    public float speed;
    public Transform[] wayPoints;
    public Transform target;
    private int destinationPoint;
    public bool activated;
    public float timeBeforeMove;

    // Varrible bubble 
    public bool haveBubble;    
    
    private void Update()
    {
        if (activated && !dead && timeBeforeMove ==0)
        {
            Patrol();
        }

        if(currentHealth <= 0)
        {
            Die();
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

    public void Die()
    {
        dead = true;
        GameObject currentRoom = GameManager.instance.whereIsPlayer();
        bool allEnemiesInRoomAreDead = currentRoom.GetComponent<Room>().patternInThisRoom.GetComponent<PatternEnemy>().PatternEnemyCleaned();
        if(allEnemiesInRoomAreDead == true)
        {
            currentRoom.GetComponent<Room>().roomFinnished = true;
            currentRoom.GetComponent<Room>().OpenDoor();
        }
    }

}