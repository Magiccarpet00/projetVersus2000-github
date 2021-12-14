using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject[] lifeUI = new GameObject[3];
    public GameObject[] munnitionUI = new GameObject[3];

    public PlayerHealth playerHealth;
    public PlayerInventory playerInventory;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    public void updateLifeUI()
    {
        int currentLife = playerHealth.currentHealth;

        for (int i = 0; i < 3; i++)
        {
            if (i < currentLife)
            {
                lifeUI[i].SetActive(true);
            }
            else
            {
                lifeUI[i].SetActive(false);
            }
        }

    }

    public void updateMunnitionUI()
    {
        int curentMunnition = playerInventory.munitionRangeAttack;

        for (int i = 0; i < 3; i++)
        {
            if (i < curentMunnition)
            {
                munnitionUI[i].SetActive(true);
            }
            else
            {
                munnitionUI[i].SetActive(false);
            }
        }


    }

}
