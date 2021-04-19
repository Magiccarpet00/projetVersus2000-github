using System.Collections.Generic;
using UnityEngine;



public class ComboManager : MonoBehaviour
{
    public Dictionary<Ressources, string> dico;
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

    public Dictionary<GameObject, float> currentTime = new Dictionary<GameObject, float>();
    public float maxTimeCombo;

    public GameObject fantomPrefab;

    public static ComboManager instance;
    private void Awake()
    {
        dico = new Dictionary<Ressources, string>();
        dico[Ressources.Hat] = "sousdossier/chapeau";
        instance = this;
    }

    private void Start()
    {
        //Joueur 1
        List<List<ComboItem>> allCombosJ1 = new List<List<ComboItem>>();
        List<ComboItem> currentComboJ1 = new List<ComboItem>();

        //Joueur 2
        List<List<ComboItem>> allCombosJ2 = new List<List<ComboItem>>();
        List<ComboItem> currentComboJ2 = new List<ComboItem>();

        playerToCombos = new Dictionary<GameObject, List<List<ComboItem>>>();
        playerToCurrentCombo = new Dictionary<GameObject, List<ComboItem>>();
        
        //Joueur 1
        playerToCombos.Add(player1, allCombosJ1);
        playerToCurrentCombo.Add(player1, currentComboJ1);

        //Joueur 2
        playerToCombos.Add(player2, allCombosJ2);
        playerToCurrentCombo.Add(player2, currentComboJ2);


        //Ajout des dictonaire pour le timer
        currentTime.Add(player1, 0f);
        currentTime.Add(player2, 0f);
    }

    private void Update()
    {
        if (currentTime[player1] > 0)
        {
            currentTime[player1] -= Time.deltaTime;
        }
        else if (currentTime[player1] > -1)
        {
            currentTime[player1] = -1;
            //Debug.Log("fin combo");
            BankCombo(player1);
        }

        if (currentTime[player2] > 0)
        {
            currentTime[player2] -= Time.deltaTime;
        }
        else if (currentTime[player2] > -1)
        {
            currentTime[player2] = -1;
            BankCombo(player2);
        }
    }

    public void AddToCombo(GameObject player)
    {
        ComboItem comboItem = new ComboItem();
        playerToCurrentCombo[player].Add(comboItem);

        currentTime[player] = maxTimeCombo;
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

        //Le mega cablage :"on recherche le nombres d'ennemy dans la room en cours"
        GameObject currentRoom = GameManager.instance.playersPosition[player];
        int nbEnemiesInRoom = currentRoom.GetComponent<Room>().
                              patternInThisRoom.GetComponent<PatternEnemy>().enemiesInPattern.Count;

        if (cloneSafe.Count == nbEnemiesInRoom)
        {
            Debug.Log("perfect");
            AttackFantom(player);
        }
    }
    
    public void AttackFantom(GameObject playerAttaquant)
    {
        GameObject playerDefenseur;

        if(player1 == playerAttaquant)
        {
            playerDefenseur = player2;
        }
        else
        {
            playerDefenseur = player1;
        }

        GameObject currentRoomPlayerDefenseur = GameManager.instance.playersPosition[playerDefenseur];

        // Un peut de random ça fait pas de mal ;)
        float rngX = Random.Range(-Constants.RANDOM_OFFSET_INSTANSIAT_FANTOM, Constants.RANDOM_OFFSET_INSTANSIAT_FANTOM);
        float rngY = Random.Range(-Constants.RANDOM_OFFSET_INSTANSIAT_FANTOM, Constants.RANDOM_OFFSET_INSTANSIAT_FANTOM);
        Vector3 offSetPossition = new Vector3(rngX, rngY, 0f);
        
        GameObject newFantom = Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Ghost]) as GameObject, currentRoomPlayerDefenseur.transform.position + offSetPossition, Quaternion.identity);
        newFantom.GetComponent<Fantom>().playerToFocus = playerDefenseur.transform;
    }

}
