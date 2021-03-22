using System.Collections.Generic;
using UnityEngine;

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
    public float offSet = 8f; // [CodeReview] Pour moi tu peux en faire une constante

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
        CreateLevelTemplate();        
    }

    private void CreateLevelTemplate()
    {
        Move(Direction.UP);

        // On commence par crée la structur de base du donjon, sans les portes ni la forme des room
        for (int i = 0; i < nbOfRooms; i++)
        {      
            CreateRoomTemplate();
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
            Move(Direction.UP);
            GameObject room = Instantiate(roomTemplate, transform.position, Quaternion.identity);
            roomsInDongeon.Add(room);
        }
    }

    // Such a baby coding... We could use anothe function with the same name in order to refactor it
    private void Move(Direction direction)
    {       
        int rng = Random.Range(0, 3);

        // C'est une petite filoutrie, pour le cas ou on veux aller forcement en up
        if(direction == Direction.UP)
        {
            rng = 1;
        }

        if(rng == 0) // LEFT
        {
            Vector2 newPos = new Vector2(transform.position.x - offSet, transform.position.y);
            transform.position = newPos;
        }

        if(rng == 1) // UP
        {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y + offSet);
            transform.position = newPos;
        }

        if(rng == 2) // RIGHT
        {
            Vector2 newPos = new Vector2(transform.position.x + offSet, transform.position.y);
            transform.position = newPos;
        }
    }
}
