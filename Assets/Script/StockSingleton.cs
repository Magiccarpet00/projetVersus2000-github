using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class StockSingleton : MonoBehaviour
{
    public static StockSingleton instance;
    public List<GameObject> allItemInGame = new List<GameObject>();
    public List<GameObject> objectToSell = new List<GameObject>();
    public GameObject shop;

    private void Awake()
    {
        for (int i = 0; i < shop.GetComponent<Shop>().slotItem.Length; i++)
        {
            int rng = Random.Range(0, allItemInGame.Count);
            objectToSell.Add(allItemInGame[rng]);
        }

        instance = this;
    }

}
    