using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int gold;
    public int munitionRangeAttack;

    // variable UI
    public Text nbUI; // A changer dans unity à charque fois


    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        nbUI.text = munitionRangeAttack.ToString();
    }
}
