using UnityEngine;
using UnityEditor;

/*
 * Les portes permettent de savoir comment deux pièces sont reliées
 * Comme ça dèse le moment ou on quitteune pièce, on sait ou ça nous amène 
 */

public class Door: MonoBehaviour
{
    public Room origine, destination;

/*
 * Cas 1 : on va de room1 à room2. 
 * On passe par cette door.
 * origine = room1;
 * destination = room2;
 * Player arrive sur door, et il nous dit ou il est.
* player est en room1. 
* on compare avec notre classe, on voit que origine = room 1 donc on retourne l'autre attribut (destination)
* player arrive sur door et il nous dit ou il est. (ecnore ..)
* player est en room2
* on compare avec notre classe, on voit que destination = room 2, donc on retourne l'autre attribut (origine)
* on lui renvoie donc la room origine = room1;

}
