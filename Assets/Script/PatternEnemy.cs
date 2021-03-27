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
    public Room patternRoom; // Permet de savoir où on est (et par extension les ennemis reliés savent où ils sont)
        
    /*
     * Update: Je me sers de cette méthode pour faire tout ce qui concerne l'initialisation ici
     */
    public void ActivationEnnemy()
    {
        for (int i = 0; i < enemiesInPattern.Count; i++)
        {
            enemiesInPattern[i].activated = true;
            enemiesInPattern[i].currentRoom = patternRoom;
        }
    }
}
