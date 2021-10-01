using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int gold;
    public int munitionRangeAttack;

    // variable UI
    //public Text nbMunitionUI; // A changer dans unity à charque fois
    //public Text nbGoldUI;


    //private void Start()
    //{
    //    UpdateUI();
    //}
    //public void UpdateUI()
    //{
    //    nbMunitionUI.text = munitionRangeAttack.ToString();
    //    nbGoldUI.text = gold.ToString();
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Coins"))
    //    {
    //        Destroy(collision.gameObject);
    //        gold++;
    //        UpdateUI();
    //    }
    //}
}
