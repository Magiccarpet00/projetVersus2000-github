using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    // La on a : "tout les item qui savent comment on les achètes"
    // C'est peut etre mieux :"l'inventaire qui sais comment on achette des items"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.GetComponent<Inventory>().gold >= price)
            {
                AddItemToInventoryPlayer(collision);
                RemoveObjectOfShop();
                collision.GetComponent<Inventory>().gold -= price;
            }            
        }
    }

    private void AddItemToInventoryPlayer(Collider2D player)
    {
        // je pense que je vais pas ajouter ce scripte a la liste Item mais un autre avec juste les info de l'item ou je sais pas
        player.GetComponent<Inventory>().items.Add(this);
    }

    private void RemoveObjectOfShop()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }



    //Cette partie devrais aller dans une classe abstrait ou un scripatable object 
    public int price;
}
