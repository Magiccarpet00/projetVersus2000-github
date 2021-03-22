using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public LevelGenerator levelGenerator;
    public GameObject startRoom;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject room = whereIsPlayer();
            Debug.Log(room);
        }
        
    }

    public GameObject whereIsPlayer()
    {
        for (int i = 0; i < levelGenerator.roomsInDongeon.Capacity; i++)
        {
            if(levelGenerator.roomsInDongeon[i].GetComponent<Room>().playerOnThisRoom == true)
            {
                return levelGenerator.roomsInDongeon[i];
            }      
        }
        return startRoom;
    }


}
