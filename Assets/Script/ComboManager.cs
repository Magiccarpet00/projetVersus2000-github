using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public Dictionary<GameObject, List<ComboItem>> playerToCombo = new Dictionary<GameObject, List<ComboItem>>();

    public GameObject player1;
    public GameObject player2;

    public static ComboManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerToCombo.Add(player1, new List<ComboItem>());
          //comboPlayerComboItem.Add(player2, comboItemJ2);
    }

    public void AddToCombo(GameObject player)
    {
        /*
         * Combo existant ? 
         * Si oui, on le rejoint
         * Sinon on en crée un nouveau.
         */
        ComboItem comboItem = new ComboItem();

        playerToCombo[player].Add(comboItem); // Je récupère la liste de combo associé à mon joueur et je rajoute un nouvel élément de combo.

        //comboItem.comboCount++;

        Debug.Log(playerToCombo[player].Count);
    }
}
