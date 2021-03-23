using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternEnemy : MonoBehaviour
{
    // On renseigne cette list depuis Unity quand on crée les patterns
    // Attention on prend l'élement enfant de la prefab Ennemy car c'est lui qui contient la classe enemy
    public List<Enemy> enemiesInPattern = new List<Enemy>();
}
