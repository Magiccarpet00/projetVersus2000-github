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

    public List<GameObject> allFloorInGame = new List<GameObject>();

    public GameObject floorShop;

    public GameObject shop;


    public Dictionary<String, GameObject> maskToDoor = new Dictionary<string, GameObject>();   

    [SerializeField] // La room utilise uniquement pour la premiere structur du donjon
    private GameObject roomTemplate;

    public List<GameObject> roomsInDongeonP1 = new List<GameObject>();
    public List<GameObject> roomsInDongeonP2 = new List<GameObject>();

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
        Move(Direction.UP,-1);
        int roomRngRange, bossRngRange, floorRngRange,
            roomRng, bossRng, floorRng;    //[Code Review] ça va faire ptet bcp de paramettre
        roomRngRange = allPatternInGame.Count;
        bossRngRange = allBossInGame.Count;
        floorRngRange = allFloorInGame.Count;
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
        for (int i = 0; i < roomsInDongeonP1.Count; i++)
        {
            // LE RANDOM TIME YOUHOU
            roomRng = UnityEngine.Random.Range(0, roomRngRange);
            bossRng= UnityEngine.Random.Range(0, bossRngRange);
            floorRng = UnityEngine.Random.Range(0, floorRngRange);
            int obstacleRng = UnityEngine.Random.Range(0, Constants.RANDOM_OBSTACLE_COUNT);

            roomsInDongeonP1[i].GetComponent<Room>().TransformationRoom(roomRng,bossRng,floorRng, obstacleRng);
            roomsInDongeonP2[i].GetComponent<Room>().TransformationRoom(roomRng, bossRng,floorRng, obstacleRng);
        }

    }

    
    private bool CreateRoomTemplate(int idRoom)
    {
        // C'est important la position du move() dans l'execution du code
        if (Physics2D.OverlapCircle(transform.position, Constants.CIRCLE_RADIUS) == null) // [Code Review] Code dupliqué
        {
            GameObject room = Instantiate(roomTemplate, transform.position, Quaternion.identity);
            GameObject room2 = Instantiate(roomTemplate, new Vector2(transform.position.x, transform.position.y + Constants.OFFSET_DONGEON), Quaternion.identity);

            room.name = "J1-Room" + idRoom.ToString();
            room2.name = "J2-Room" + idRoom.ToString();

            int rng = UnityEngine.Random.Range(0, 2);
            Move(Direction.RNG, rng);
            roomsInDongeonP1.Add(room);
            roomsInDongeonP2.Add(room2);
            TypeOfRoom(idRoom, room);
            TypeOfRoom(idRoom, room2); 
            return true; 
        }
        else
        {            
            Move(Direction.UP, -1);
            return false;
        }
    }

    private void Move(Direction direction, int rng)
    {
        Direction final = default; // permet de remplacer une déclaration à null qui fera hurler le compilateur
        if(direction == Direction.RNG)
        {
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
