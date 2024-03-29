﻿using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    public BulletBehaviour bulletBehaviour;
    public int damage;
    public float speed;
    public Vector2 dirrectionBullet;
    public GameObject player;

    // TARGET
    public GameObject playerToFocus;    
    public float accuracyAmount; // plus il est proche de 0 plus c'est precis, il devrait etre dynamique en fonction de la distance
    public float accuracy; // 1 = sa change rien --- 0.6 = c'est bof precis
        

    public void Start()
    {
        if(bulletBehaviour == BulletBehaviour.TARGET)
        {
            DirectionTargetBulletCalcule();
        }
        else if(bulletBehaviour == BulletBehaviour.DIRECTIONAL)
        {
            
        }
        
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                 new Vector2(transform.position.x + dirrectionBullet.x,
                                                             transform.position.y + dirrectionBullet.y),
                                                 speed * Time.deltaTime);
    }

    public void DirectionTargetBulletCalcule()
    {
        // Calcule pour l'accuracy
        Vector2 target = new Vector2(playerToFocus.transform.position.x, playerToFocus.transform.position.y);
        float distance = Vector2.Distance(transform.position, target);
        accuracyAmount = (distance * Constants.ACCURACY_MODIFICATEUR) / accuracy;

        // Le vrais calcul de la target
        float rngX = Random.Range(-accuracyAmount, accuracyAmount);
        float rngY = Random.Range(-accuracyAmount, accuracyAmount);
        target = new Vector2(playerToFocus.transform.position.x + rngX, playerToFocus.transform.position.y + rngY);

        dirrectionBullet = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        dirrectionBullet = dirrectionBullet.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Bubble"))
        {
            StopBullet();
        }

        if (collision.CompareTag("Player"))
        {
            if(collision.gameObject != player)
            {
                if (collision.GetComponent<PlayerHealth>().isInvincible == false)
                {
                    StartCoroutine(collision.GetComponent<PlayerHealth>().TakeDamage(dirrectionBullet, damage));
                    StopBullet();
                }
            }            
        }
    }

    private void StopBullet()
    {
        speed = 0;
        GetComponent<Animator>().SetTrigger("hit");
        // Il faut que hit sois generique a toute les bullets
    }
}
