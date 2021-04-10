using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public Dictionary<GameObject, List<List<ComboItem>>> playerToCombos;
    public Dictionary<GameObject, List<ComboItem>> playerToCurrentCombo;
    /*
     * Joueur bute 1 monstre, puis un puis un, et 1 seconde passe -> Combo 1 crée et terminé $1
     * Joueur bute 1 monstre puis 1 seconde passe -> Combo 2 crée et terminé $2
     * 
     * List<List<ComboItem>> allCombos = new List<List<ComboItem>>();
     * 
     * Début $1
     * ComboItem comboItem1, comboItem2, comboItem3;
     * List<ComboItem> combo1 = new List<ComboItem>();
     * comboItem1 = new ComboItem();
     * comboItem2 = new ComboItem();
     * comboItem3 = new ComboItem();
     * combo1.Add(comboItem1);
     * combo1.Add(comboItem2);
     * combo1.Add(comboItem3);
     * allCombos.add(combo1);
     * Fin $1
     * 
     * Début $2
     * List<ComboItem> combo2 = new List<ComboItem>();
     * comboItem4 = new ComboItem();
     * combo2.Add(ComboItem4);
     * allCombos.add(combo2);
     * Fin $2
     * 
     * On a maintenant tous les combo crée dans la partie, et pour chacun d'eux l'ensemble des combo items.
    */
    public GameObject player1;
    public GameObject player2;

    public static ComboManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        List<List<ComboItem>> allCombosJ1 = new List<List<ComboItem>>();
        List<ComboItem> currentComboJ1 = new List<ComboItem>();
        playerToCombos = new Dictionary<GameObject, List<List<ComboItem>>>();
        playerToCurrentCombo = new Dictionary<GameObject, List<ComboItem>>();
   

        playerToCombos.Add(player1, allCombosJ1);
        playerToCurrentCombo.Add(player1, currentComboJ1);

        //comboPlayerComboItem.Add(player2, comboItemJ2);
    }

    public void AddToCombo(GameObject player)
    {
        ComboItem comboItem = new ComboItem();
        /*
         * Combo existant ? 
         * Si oui, on le rejoint
         * Sinon on en crée un nouveau.
         */


        playerToCombo[player].Add(comboItem); // Je récupère la liste de combo associé à mon joueur et je rajoute un nouvel élément de combo.

        //comboItem.comboCount++;

        Debug.Log(playerToCombo[player].Count);
    }
}
