using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class LevelGenerator : MonoBehaviour
{
    //--------Room-------------
    // La liste des games objects room que j'ai remplie dans Unity
    public GameObject UP, RIGHT, LEFT, DOWN,
                       UP_RIGHT, RIGHT_DOWN, DOWN_LEFT, LEFT_UP, UP_DOWN, LEFT_RIGHT,
                       UP_RIGHT_DOWN, RIGHT_DOWN_LEFT, DOWN_LEFT_UP, LEFT_UP_RIGHT,
                       UP_RIGHT_DOWN_LEFT;    
    enum Direction
    {
        UP,
        LEFT,
        DOWN,
        RIGHT,
        RNG
    }

    [SerializeField] // La room utilise uniquement pour la premiere structur du donjon
    private GameObject roomTemplate;

    public List<GameObject> roomsInDongeon = new List<GameObject>();

//--------Variable-------------

    [SerializeField]    
    private int nbOfRooms = 10;


//----------------------------------
    public static LevelGenerator instance;
    private void Awake()
    {
        instance = this;
    }
//----------------------------------


    private void Start()
    {
        StartCoroutine(CreateLevelTemplate());        
    }

    

    IEnumerator CreateLevelTemplate()
    {
        Move(Direction.UP);

        // On commence par crée la structur de base du donjon, sans les portes ni la forme des room
        for (int i = 0; i < nbOfRooms; i++)
        {      
            CreateRoomTemplate();
            yield return new WaitForSeconds(.1f);
        }

        // Ici on va transformé les roomTemplate en room avec des ouvertures sur les cotés
        for (int i = 0; i < roomsInDongeon.Count; i++)
        {
            roomsInDongeon[i].GetComponent<Room>().TransformationRoom();
        }
    }

    private void CreateRoomTemplate()
    {
        // C'est important la position du move() dans l'execution du code

        if (Physics2D.OverlapCircle(transform.position, Constants.CIRCLE_RADIUS) == null)
            // [CodeReview] je me suis permi de le refactorisé dans une autre classe comme on a dit, vu que tu l'utilise partout
        {
            GameObject room = Instantiate(roomTemplate, transform.position, Quaternion.identity);
            Move(Direction.RNG);
            roomsInDongeon.Add(room);
        }
        else
        {
            nbOfRooms++; // [CodeReview] On augmente le nombre total de room à créer au cas où on a pas réussi (sinon on peut se retrouver avec 8 rooms sur 10) 
            Move(Direction.UP);
        }
    }

    private void Move(Direction direction)
    {       
        int rng = Random.Range(0, 2);

        // C'est une petite filoutrie, pour le cas ou on veux aller forcement en up
         /*
         [CodeReview] A REVOIR !!!!!!
         */
        if(direction == Direction.UP)
        {
            rng = 2;
        }

        if(rng == 0) // LEFT
        {
            Vector2 newPos = new Vector2(transform.position.x - Constants.OFFSET, transform.position.y);
            transform.position = newPos;
        }

        if(rng == 2) // UP
        {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y + Constants.OFFSET);
            transform.position = newPos;
        }

        if(rng == 1) // RIGHT
        {
            Vector2 newPos = new Vector2(transform.position.x + Constants.OFFSET, transform.position.y);
            transform.position = newPos;
        }
    }
}
