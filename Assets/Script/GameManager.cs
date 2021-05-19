﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private void Awake()
    {
        instance = this;
        PrefabFinder.Init();
    }

    // Provisoir je pense, c'est pour le follow de la camera
    // public Room playerPosition;

    // Pour la possiton des player
    public GameObject[] players;
    
    // C'est une variable qui garde en memoire
    // dans quelle room se trouve chaque joueur <player, room>
    public Dictionary<GameObject, GameObject> playersPosition = new Dictionary<GameObject, GameObject>();

    //Ref au autre partit du code
    public LevelGenerator levelGenerator;
    public GameObject[] startRooms;

    private void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            playersPosition.Add(players[i], startRooms[i]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < players.Length; i++)
            {
                Debug.Log(playersPosition[players[i]]);
                GameObject go = playersPosition[players[i]];
                Room r = playersPosition[players[i]].GetComponent<Room>();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("enter");
            SceneManager.LoadScene(0); // ça bug defois
        }
    }
}

// [memo]
/* -les enemy sont is_trigger
 * -le petit fantom sont is_trigger
 * -le bullet sont is_trigger
 * 
 * -le joueur est solid avec rb
 * -l'epée est is_trigger avec rb 
 * */
