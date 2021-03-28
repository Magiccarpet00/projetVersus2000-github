using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Tout les items du jeux renseigné dans unity
    public List<GameObject> allItemInGame = new List<GameObject>();

    // La position ou apparaissent les item dans le jeu
    public Transform[] slotItem;

    //La liste des item vendu pas le marchant
    public List<GameObject> itemToSell = new List<GameObject>();

    private void Start()
    {
       
        for (int i = 0; i < slotItem.Length; i++)
        {
            GameObject tmp = StockSingleton.instance.objectToSell[i];
            GameObject item = Instantiate(tmp, slotItem[i].position, Quaternion.identity);
            item.transform.parent = this.transform;
            itemToSell.Add(item);
        }

    }
}
