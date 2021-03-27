using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatternEnemy : MonoBehaviour
{
    // On renseigne cette list depuis Unity quand on crée les patterns
    // Attention on prend l'élement enfant de la prefab Ennemy car c'est lui qui contient la classe enemy
    public List<Enemy> enemiesInPattern = new List<Enemy>();
    public int maxEnemies;
        

    public void ActivationEnnemy()
    {
        for (int i = 0; i < enemiesInPattern.Count; i++)
        {
            enemiesInPattern[i].activated = true;
        }
    }

    public bool PatternEnemyCleaned() //[code review]
    {
        int nb = 0;
        for (int i = 0; i < enemiesInPattern.Count; i++)
        {
            if (enemiesInPattern[i].dead == true)
            {
                nb++;
            }
        }

        if(nb == enemiesInPattern.Count)
        {
            Debug.Log("room fini");
            return true;
        }
        else
        {
            Debug.Log("room pas fini");
            return false;
        }
    }
}
