using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;

public class LevelGenerator : MonoBehaviour
{
    //--------Room-------------
    // La liste des games objects room que j'ai remplie dans Unity
    public GameObject UP, RIGHT, LEFT, DOWN,
                       UP_RIGHT, RIGHT_DOWN, DOWN_LEFT, LEFT_UP, UP_DOWN, LEFT_RIGHT,
                       UP_RIGHT_DOWN, RIGHT_DOWN_LEFT, DOWN_LEFT_UP, LEFT_UP_RIGHT,
                       UP_RIGHT_DOWN_LEFT;

    // La liste de tous les pattern de monstre que j'ai remplie dans Unity
    public List<GameObject> allPatternInGame = new List<GameObject>();

    public List<GameObject> allBossInGame = new List<GameObject>();

    public GameObject shop;


    enum Direction
    {
        UP,
        LEFT,
        DOWN,
        RIGHT,
        RNG
    }

    public Dictionary<String, GameObject> maskToDoor = new Dictionary<string, GameObject>();   

    [SerializeField] // La room utilise uniquement pour la premiere structur du donjon
    private GameObject roomTemplate;

    public List<GameObject> roomsInDongeon = new List<GameObject>();

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
        FillMaskToSprite();
        DuplicateDongeon();
    }

    private void FillMaskToSprite()
    {
        maskToDoor["1000"] = LevelGenerator.instance.RIGHT;
        maskToDoor["0100"] = LevelGenerator.instance.DOWN;
        maskToDoor["0010"] = LevelGenerator.instance.LEFT;
        maskToDoor["0001"] = LevelGenerator.instance.UP;
        maskToDoor["1001"] = LevelGenerator.instance.UP_RIGHT;
        maskToDoor["1100"] = LevelGenerator.instance.RIGHT_DOWN;
        maskToDoor["0110"] = LevelGenerator.instance.DOWN_LEFT;
        maskToDoor["0011"] = LevelGenerator.instance.LEFT_UP;
        maskToDoor["0101"] = LevelGenerator.instance.UP_DOWN;
        maskToDoor["1010"] = LevelGenerator.instance.LEFT_RIGHT;
        maskToDoor["1101"] = LevelGenerator.instance.UP_RIGHT_DOWN;
        maskToDoor["1110"] = LevelGenerator.instance.RIGHT_DOWN_LEFT;
        maskToDoor["0111"] = LevelGenerator.instance.DOWN_LEFT_UP;
        maskToDoor["1011"] = LevelGenerator.instance.LEFT_UP_RIGHT;
        maskToDoor["1111"] = LevelGenerator.instance.UP_RIGHT_DOWN_LEFT;
    }

    IEnumerator CreateLevelTemplate()
    {
        Move(Direction.UP);

        // On commence par crée la structur de base du donjon, sans les portes ni la forme des room
        int id = 0;
        while (id < nbOfRooms)
        {
            bool createdRoom = CreateRoomTemplate(id);

            if (createdRoom == true)
            {
                id++;
            }
            yield return new WaitForSeconds(.1f);
        }       

        //Ici on va transformé les roomTemplate en room avec des ouvertures sur les cotés
        for (int i = 0; i < roomsInDongeon.Count; i++)
        {
            roomsInDongeon[i].GetComponent<Room>().TransformationRoom();
        }
        DuplicateDongeon();
    }

    public void DuplicateDongeon()
    {
        for (int i = 0; i < roomsInDongeon.Count; i++)
        {
            Instantiate(roomsInDongeon[i], new Vector3(roomsInDongeon[i].transform.position.x,
                                                       roomsInDongeon[i].transform.position.y + Constants.OFFSET_DONGEON,
                                                       roomsInDongeon[i].transform.position.z),
                                                       Quaternion.identity);
        }
    }

    private bool CreateRoomTemplate(int idRoom)
    {
        // C'est important la position du move() dans l'execution du code
        if (Physics2D.OverlapCircle(transform.position, Constants.CIRCLE_RADIUS) == null)
        {
            GameObject room = Instantiate(roomTemplate, transform.position, Quaternion.identity);
            room.name = "Room" + idRoom.ToString();
            Move(Direction.RNG);
            roomsInDongeon.Add(room);
            TypeOfRoom(idRoom, room);
            return true;
        }
        else
        {            
            Move(Direction.UP);
            return false;
        }
    }

    private void Move(Direction direction)
    {
        Direction final = default; // ;-) permet de remplacer une déclaration à null qui fera hurler le compilateur
        if(direction == Direction.RNG)
        {
           int rng = UnityEngine.Random.Range(0, 2);
        switch (rng)
        {
            case 0:
                final = Direction.LEFT;
                break;
            case 1:
                final = Direction.RIGHT;
                break;
        }
        }
        else
        {
            final = direction; // Direction.UP par défaut
        }

        if(final == Direction.LEFT) 
        {
            Vector2 newPos = new Vector2(transform.position.x - Constants.OFFSET, transform.position.y);
            transform.position = newPos;
        }

        if (final == Direction.UP) 
        {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y + Constants.OFFSET);
            transform.position = newPos;
        }

        if (final == Direction.RIGHT)
        {
            Vector2 newPos = new Vector2(transform.position.x + Constants.OFFSET, transform.position.y);
            transform.position = newPos;
        }
    }

    private void TypeOfRoom(int idRoom, GameObject room) 
    {
        if(idRoom == Constants.SHOP_ROOM) // 
        {
            //Shop room
            room.GetComponent<Room>().typeRoom = TypeRoom.SHOP;
        }
        else if(idRoom == nbOfRooms-1)
        {
            //Boss room
            room.GetComponent<Room>().typeRoom = TypeRoom.BOSS;
        }
        else
        {
            // Classique room
            room.GetComponent<Room>().typeRoom = TypeRoom.VANILLA;
        }
    }

    
}
