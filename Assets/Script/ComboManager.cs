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

    public List<GameObject> wattingListAttackVersusJ1 = new List<GameObject>();



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



        // ------POUR LES GIGA TEST------

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AttackVersus(player1));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            player2.GetComponent<PlayerCharacter>().goAttackVersus = 1;
        }

        // --------------------------------
    }

    public void AddToCombo(GameObject player)
    {
        ComboItem comboItem = new ComboItem();
        playerToCurrentCombo[player].Add(comboItem);

        currentTime[player] = maxTimeCombo;

        PerfectCheck(player);
    }

    public void PerfectCheck(GameObject _player)
    {
        //[REFACTOT] j'ai mis ça ici pour que le perfect parte directe 
        GameObject currentRoom = GameManager.instance.playersPosition[_player];
        int nbEnemiesInRoom = currentRoom.GetComponent<Room>().
                              patternInThisRoom.GetComponent<PatternEnemy>().enemiesInPattern.Count;

        if (playerToCurrentCombo[_player].Count == nbEnemiesInRoom)
        {


            StartCoroutine(AttackVersus(_player));

            // Visuel sympa
            Instantiate(Resources.Load(PrefabFinder.RessourcesToURI[Ressources.Pentagrame]) as GameObject,
                                       _player.transform.position,
                                       Quaternion.identity);
        }
    }

    public void BankCombo(GameObject player)
    {
        /*
         * WARNING TUTO PASSAGE PAR REFERENCE
         */
        //playerToCombos[player].Add(playerToCurrentCombo[player]);  // oh yeah j'ai ajouté mon combo actuel à ma liste des combos
        //playerToCurrentCombo[player].Clear(); 

        List<ComboItem> cloneSafe = new List<ComboItem>(playerToCurrentCombo[player]);
        playerToCombos[player].Add(cloneSafe);
        playerToCurrentCombo[player].Clear();
        /* Todo: Ne pas ajouter de combo vides (Liste 0 combo item)*/

        //int i = 1;
        //Debug.Log("Stockage d'un nouveau combo. Combo stockés:");
        //foreach (var item in playerToCombos[player])
        //{
        //    Debug.Log("Combo: " + i + " de " + item.Count);
        //    i++;
        //}
    }
    
    public System.Collections.IEnumerator AttackVersus(GameObject playerAttaquant)
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

        // On regarde si on fait l'attack versus mnt our si on la declanche plus tard
        if(currentRoomPlayerDefenseur.GetComponent<Room>().roomFinnished == true)
        {
            // [Code Panique] C'est pas ma faute c'est la doc qui a dit de faire commme ça, moi je voulais un bool
            yield return new WaitUntil(() => playerDefenseur.GetComponent<PlayerCharacter>().goAttackVersus >= 1);

            playerDefenseur.GetComponent<PlayerCharacter>().goAttackVersus = 0;

            currentRoomPlayerDefenseur = GameManager.instance.playersPosition[playerDefenseur];
            StartCoroutine(AttackVersusCharacter(playerAttaquant, playerDefenseur, currentRoomPlayerDefenseur));

        }
        else if(currentRoomPlayerDefenseur.GetComponent<Room>().roomFinnished == false)
        {
            StartCoroutine(AttackVersusCharacter(playerAttaquant, playerDefenseur, currentRoomPlayerDefenseur));
        }
    }

    public System.Collections.IEnumerator AttackVersusCharacter(GameObject playerAttaquant, GameObject playerDefenseur, GameObject currentRoomPlayerDefenseur)
    {
        PlayerCharacter playerCharacter = playerAttaquant.GetComponent<PlayerCharacter>();

        if (playerCharacter.character == Character.BLUE)
        {
            float rngX = Random.Range(-2f, 2f);
            float rngY = Random.Range(-2f, 2f);
            Vector3 offSetPossition = new Vector3(rngX, rngY, 0f);

            GameObject newAttackVersus = Instantiate(playerCharacter.versusAttackPrefab,
                                                     currentRoomPlayerDefenseur.transform.position + offSetPossition,
                                                     Quaternion.identity);

            AttackVersus attackVersus = newAttackVersus.GetComponent<AttackVersus>();

            attackVersus.playerToFocus = playerDefenseur;
            attackVersus.player = playerAttaquant;
            attackVersus.currentRoom = currentRoomPlayerDefenseur;
        }

        else if (playerCharacter.character == Character.RED)
        {
            List<AttackVersus> listAttackVerus = new List<AttackVersus>();

            for (int i = 0; i < playerCharacter.redVersusAttackCount; i++)
            {
                float rngX = Random.Range(-3f, 3f);
                float rngY = Random.Range(-3f, 1.5f);
                Vector3 offSetPossition = new Vector3(rngX, rngY, 0f);

                GameObject newAttackVersus = Instantiate(playerCharacter.versusAttackPrefab,
                                                     currentRoomPlayerDefenseur.transform.position + offSetPossition,
                                                     Quaternion.identity);

                AttackVersus attackVersus = newAttackVersus.GetComponent<AttackVersus>();

                listAttackVerus.Add(attackVersus);

                attackVersus.playerToFocus = playerDefenseur;
                attackVersus.player = playerAttaquant;
                attackVersus.currentRoom = currentRoomPlayerDefenseur;
                attackVersus.redDamage = playerCharacter.redVersusAttackDamage;

                //---------Pour L'invocation asyncrhone---------------

                // [Code Panique] en plus ça marche pas
                float rngTime = 0.5f;
                //float rngTime = Random.Range(0.2f, 0.8f);

                if (i == 0)
                {
                    //listAttackVerus[0].offSetLifeTime = rngTime;
                    listAttackVerus[0].offSetLifeTime = 0.5f;
                }
                else if (i == 1)
                {
                    //listAttackVerus[1].offSetLifeTime = rngTime + listAttackVerus[0].offSetLifeTime;
                    listAttackVerus[1].offSetLifeTime = 1f;
                }
                else if (i == 2)
                {
                    //listAttackVerus[2].offSetLifeTime = rngTime + listAttackVerus[1].offSetLifeTime + listAttackVerus[0].offSetLifeTime;
                    listAttackVerus[2].offSetLifeTime = 1.5f;
                }
                yield return new WaitForSeconds(rngTime);
            }
        }

        else if (playerCharacter.character == Character.GREEN)
        {

        }
    }
}
