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

        // Todo joueur 2
        //comboPlayerComboItem.Add(player2, comboItemJ2);
    }

    public void AddToCombo(GameObject player)
    {
        ComboItem comboItem = new ComboItem();
        playerToCurrentCombo[player].Add(comboItem);
    }

    public void BankCombo(GameObject player)
    {
        /*
         * WARNING TUTO PASSAGE PAR REFERENCE
         */
        //playerToCombos[player].Add(playerToCurrentCombo[player]);  // oh yeah j'ai ajouté mon combo actuel à ma liste des combos :-) cool cool 
        //playerToCurrentCombo[player].Clear(); 

        List<ComboItem> cloneSafe = new List<ComboItem>(playerToCurrentCombo[player]);
        playerToCombos[player].Add(cloneSafe);
        playerToCurrentCombo[player].Clear();
        /* Todo: Ne pas ajouter de combo vides (Liste 0 combo item)*/

        int i = 1;
        Debug.Log("Stockage d'un nouveau combo. Combo stockés:");
        foreach (var item in playerToCombos[player])
        {
            Debug.Log("Combo: " + i + " de " + item.Count);
            i++;
        }

    }
}
