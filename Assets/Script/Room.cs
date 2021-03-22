using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
//-----------Creation de la structure des rooms-------------
    public bool openRight;    
    public bool openLeft;    
    public bool openUp;    
    public bool openDown;

    public GameObject doorRight, doorLeft, doorUp, doorDown;
    public List<GameObject> doorsInRoom = new List<GameObject>();
    public Dictionary<String, Sprite> maskToDoor = new Dictionary<string, Sprite>();
   

    public void TransformationRoom()
    {
        // Right > Down > Left > Up
        maskToDoor["1000"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["0100"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["0010"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["0001"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["1001"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["1100"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["0110"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["0011"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["0101"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["1010"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["1101"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["1110"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["0111"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["1011"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        maskToDoor["1111"] = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
        //Cette methode va rensegné les boolean ci-dessus
        ApertureCheck();

        // 1 ouverture
        // [CodeReview] Comment refactoriser ?
        {
            /*
             * [Explications] Pour chaque room on va chercher le sprite correspondant en testant toutes les ouvertures
             * */

            /*
             * ULDR
             * 0001
             * 0010
             * 0100
             * 1000
             * 0001
             */
            if (openRight && !openDown && !openLeft && !openUp) // 0001
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.RIGHT.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorRight, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
            }
            if (!openRight && openDown && !openLeft && !openUp) // 0010
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.DOWN.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorDown, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
            }
            if (!openRight && !openDown && openLeft && !openUp) // 0100
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.LEFT.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorLeft, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
            }
            if (!openRight && !openDown && !openLeft && openUp) // 1000
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.UP.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorUp, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
            }
        }

        // 2 ouvertures
        {            
            if (openRight && !openDown && !openLeft && openUp) // 1001
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.UP_RIGHT.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorUp, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorRight, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
            }
            if (openRight && openDown && !openLeft && !openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.RIGHT_DOWN.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorRight, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorDown, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
            }
            if (!openRight && openDown && openLeft && !openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.DOWN_LEFT.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorDown, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorLeft, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
            }
            if (!openRight && !openDown && openLeft && openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.LEFT_UP.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorLeft, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorUp, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
            }
            if (!openRight && openDown && !openLeft && openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.UP_DOWN.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorUp, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorDown, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
            }
            if (openRight && !openDown && openLeft && !openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.LEFT_RIGHT.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorLeft, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorRight, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
            }
        }

        // 3 ouvertures
        {
            if (openRight && openDown && !openLeft && openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.UP_RIGHT_DOWN.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorUp, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorRight, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
                GameObject door3 = Instantiate(doorDown, transform.position, Quaternion.identity);
                doorsInRoom.Add(door3);
            }
            if (openRight && openDown && openLeft && !openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.RIGHT_DOWN_LEFT.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorLeft, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorRight, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
                GameObject door3 = Instantiate(doorDown, transform.position, Quaternion.identity);
                doorsInRoom.Add(door3);
            }
            if (!openRight && openDown && openLeft && openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.DOWN_LEFT_UP.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorUp, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorLeft, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
                GameObject door3 = Instantiate(doorDown, transform.position, Quaternion.identity);
                doorsInRoom.Add(door3);
            }
            //bretzels and chill
            if (openRight && !openDown && openLeft && openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.LEFT_UP_RIGHT.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorUp, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorRight, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
                GameObject door3 = Instantiate(doorLeft, transform.position, Quaternion.identity);
                doorsInRoom.Add(door3);
            }
        }

        // 4 ouverture
        {
            if (openRight && openDown && openLeft && openUp)
            {
                GetComponent<SpriteRenderer>().sprite = LevelGenerator.instance.UP_RIGHT_DOWN_LEFT.GetComponent<SpriteRenderer>().sprite;
                GameObject door = Instantiate(doorUp, transform.position, Quaternion.identity);
                doorsInRoom.Add(door);
                GameObject door2 = Instantiate(doorRight, transform.position, Quaternion.identity);
                doorsInRoom.Add(door2);
                GameObject door3 = Instantiate(doorDown, transform.position, Quaternion.identity);
                doorsInRoom.Add(door3);
                GameObject door4 = Instantiate(doorLeft, transform.position, Quaternion.identity);
                doorsInRoom.Add(door4);
            }
        }


        /*
         * Création des doors selons le mask
         */
        String s = "1000"; // 1001 // 1111
        char[] array = s.ToCharArray();
        for (int i = 0; i < array.Length; i++)
        {
            char c = array[i];
            if (c == '1') // Si on a 1, alors ça veut dire qu'il faut créer une porte
            {
                GameObject door;
                switch (i) // Selon notre position dans le mask on sait si il faut la faire en haut, bas, gauche, droite
                {
                    case 0:
                        door = Instantiate(door, transform.position, Quaternion.identity);
                        break;
                    case 1:
                        door = Instantiate(door, transform.position, Quaternion.identity);
                        break;
                    case 2:
                        door = Instantiate(door, transform.position, Quaternion.identity);
                        break;
                    case 3:
                        door = Instantiate(door, transform.position, Quaternion.identity);
                        break;
                }
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

        if (Physics2D.OverlapCircle(rightCheck, Constants.CIRCLE_RADIUS) != null)
        {
            openRight = true;
        }

        if (Physics2D.OverlapCircle(leftCheck, Constants.CIRCLE_RADIUS) != null)
        {
            openLeft = true;
        }

        if (Physics2D.OverlapCircle(upCheck, Constants.CIRCLE_RADIUS) != null)
        {
            openUp = true;
        }

        if (Physics2D.OverlapCircle(downCheck, Constants.CIRCLE_RADIUS) != null)
        {
            openDown = true;
        }
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
//----------------------------------------------------------








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
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnThisRoom = false;
        }
    }
//----------------------------------------------------------





}
