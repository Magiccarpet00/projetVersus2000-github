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

    // Varriable patrol
    public float speed;
    public Transform[] wayPoints;
    private Transform target;
    private int destinationPoint;
    public bool activated;

    // Varrible bubble 
    public bool haveBubble;

    public void Start()
    {        
        target = wayPoints[0];
    }

    private void Update()
    {
        if (activated && !dead)
        {
            Patrol();
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

}
