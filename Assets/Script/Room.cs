﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
//-----------Creation de la structure des rooms-------------
    public bool openRight;    
    public bool openLeft;    
    public bool openUp;    
    public bool openDown;
    public String mask;

    public GameObject doorRight, doorLeft, doorUp, doorDown;
    public List<GameObject> doorsInRoom = new List<GameObject>();
    //public Dictionary<String, Sprite> maskToDoor = new Dictionary<string, Sprite>();


    public void TransformationRoom()
    {
        /* Right > Down > Left > Up
         * Création de notre dictionnaire pour le mask. Une entrée = un layout en sortie. */


        /*
         * Traduction de nos booleans d'entrées en mask 
         * */
        //Cette methode va rensegné les boolean ci-dessus
        ApertureCheck();
        ChangeTypeOfRoom();

        GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.maskToDoor[mask];

        // Création des doors selons le mask
        char[] array = mask.ToCharArray(); // On découpe notre mask en un tableau pour pouvoir récupérer individuellement chaque caractère (= char). 
        for (int i = 0; i < array.Length; i++)
        {
            char c = array[i]; // On garde en mémoire la caractère sur lequel on est...
            if (c == '1') // Si on a 1, alors ça veut dire qu'il faut créer une porte (mais on ne sait pas encore à quel endroit)
            {
                GameObject door = doorUp; // Bensie Filouterie... sorry... I didn't want to duplicate but I was forced

                /*
                 * Chaque caractère de notre mask correspond à une position dans la room.
                 * Car 1: Porte de droite
                 * Car 2: Porte du bas
                 * Car 3: Porte de gauche
                 * Car 4: Porte du haut
                 * 
                 * On enlève 1 à chaque fois car c'est un tableau
                 * Ensuite dans chaque itération, on regarde la valeur de i qui nous dit où on en est dans le string. ça nous permet de savoir
                 * ou instancier la porte si il y en a une.
                 */
                switch (i) // Selon notre position dans le mask on sait si il faut la faire en haut, bas, gauche, droite
                           //Right > Down > Left > Up
                {
                    case 0:
                        door = Instantiate(doorRight, transform.position, Quaternion.identity); 
                        
                        break;
                    case 1:
                        door = Instantiate(doorDown, transform.position, Quaternion.identity);
                        break;
                    case 2:
                        door = Instantiate(doorLeft, transform.position, Quaternion.identity);
                        break;
                    case 3:
                        door = Instantiate(doorUp, transform.position, Quaternion.identity);
                        break;
                    default:
                        Debug.LogError("Benoit is a looser");
                        break;
                }
                door.transform.parent = this.transform;
                doorsInRoom.Add(door);
            }
        }
    
    // On ouvre toute les portes
    OpenDoor();        
    }
    public void ApertureCheck()
    {
        /*
         * [Explications] On test si on à des templates voisins avec une boîte de collision. On modifie juste les bools 
         */
        Vector2 rightCheck = new Vector2(transform.position.x + Constants.OFFSET, transform.position.y);
        Vector2 leftCheck = new Vector2(transform.position.x - Constants.OFFSET, transform.position.y);
        Vector2 upCheck = new Vector2(transform.position.x, transform.position.y + Constants.OFFSET);
        Vector2 downCheck = new Vector2(transform.position.x, transform.position.y - Constants.OFFSET);

        mask +=  
            (Physics2D.OverlapCircle(rightCheck, Constants.CIRCLE_RADIUS) != null) ? // condition qu'on test
            "1" // valeur renvoyée si condition vraie
            : "0"; // Valeur renvoyée si condition fausse

        mask += (Physics2D.OverlapCircle(downCheck, Constants.CIRCLE_RADIUS) != null) ? "1" : "0";
        mask += (Physics2D.OverlapCircle(leftCheck, Constants.CIRCLE_RADIUS) != null) ? "1" : "0";
        mask += (Physics2D.OverlapCircle(upCheck, Constants.CIRCLE_RADIUS) != null) ? "1" : "0";

        // A la fin notre mask sera bienune chaîne de 4 


    }

    public void OpenDoor()
    {
        for (int i = 0; i < doorsInRoom.Count; i++)
        {
            doorsInRoom[i].gameObject.SetActive(false);
        }
    }

    public void CloseDoor()
    {
        for (int i = 0; i < doorsInRoom.Count; i++)
        {
            doorsInRoom[i].gameObject.SetActive(true);
        }
    }

    //-----------Detection du joueur de dans la room------------

    // Ici on peut se retrouver dans le cas ou le joueur est dans 2 rooms à la fois quand il est entre 2 rooms
    // Je ferrais des ferification au moment des super pour savoir si le joueur n'est pas entre les rooms

    public bool playerOnThisRoom; // Cette varriable est accsesible depuis LevelGenerator dans la liste roomsInDugeon
    public bool roomFinnished;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnThisRoom = true;

            // Si on revient dans une room qu'on a fini on reste enfermé dedant sinon
            if (roomFinnished == false)
            {
                CloseDoor();
            }

            if (classicRoom && roomFinnished == false)
            {
                patternInThisRoom.GetComponent<PatternEnemy>().ActivationEnnemy();
            }           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnThisRoom = false;
        }
    }


    //-----------Creation des Pattern d'enemies-----------------

    // Je voulais le faire avec des enums mais je comprend pas, ou ptet des classes 
    // abstraite, du coup je vais faire ça comme un sagouin et on verra en code CodeReview

    public bool shopRoom;
    public bool bossRoom;
    public bool classicRoom;

    public GameObject patternInThisRoom;

    public void ChangeTypeOfRoom()
    {
        if (shopRoom)
        {
            roomFinnished = true;
            GameObject shop = Instantiate(LevelGenerator.instance.shop, transform.position, Quaternion.identity);
            shop.transform.parent = this.transform;
        }

        if (bossRoom)
        {
            int rng = UnityEngine.Random.Range(0, LevelGenerator.instance.allBossInGame.Count);
            GameObject bossInRoom = Instantiate(LevelGenerator.instance.allBossInGame[rng], transform.position, Quaternion.identity);
            bossInRoom.transform.parent = this.transform;
        }

        if (classicRoom)
        {
            int rng = UnityEngine.Random.Range(0, LevelGenerator.instance.allPatternInGame.Count);
            patternInThisRoom = Instantiate(LevelGenerator.instance.allPatternInGame[rng], transform.position, Quaternion.identity);
            patternInThisRoom.transform.parent = this.transform;
        }
    }
}
