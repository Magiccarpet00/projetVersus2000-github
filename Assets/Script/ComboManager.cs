using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public Dictionary<GameObject, ComboItem> comboPlayerComboItem = new Dictionary<GameObject, ComboItem>();

    public GameObject player1;
    public GameObject player2;

    public ComboItem comboItemJ1;
    public ComboItem comboItemJ2;

    public static ComboManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       
          comboPlayerComboItem.Add(player1, comboItemJ1);
          comboPlayerComboItem.Add(player2, comboItemJ2);
    }

    public void StartCombo(GameObject player)
    {
        ComboItem comboItem = comboPlayerComboItem[player];
        comboItem.comboCount++;

        Debug.Log(comboItem.comboCount);
    }
}
