using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Room roomOrigine;
    public Room roomDestination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Debug.Log("Je viens de " + roomOrigine + " et vais vers " + roomDestination);
            if (GameManager.instance.playersPosition[collision.gameObject].GetComponent<Room>() == roomOrigine)
            {
                Debug.Log("Je viens de " + roomOrigine + " et vais vers " + roomDestination);

                collision.GetComponent<PlayerMovement>().TapisRoulant(roomOrigine, roomDestination);
            }
            //Debug.Log("Je viens de " + roomDestination + " et vais vers " + roomOrigine);
            else if (GameManager.instance.playersPosition[collision.gameObject].GetComponent<Room>() == roomDestination)
            {
                Debug.Log("Je viens de " + roomDestination + " et vais vers " + roomOrigine);

                collision.GetComponent<PlayerMovement>().TapisRoulant(roomDestination, roomOrigine);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().isBetweenRooms = false;
            collision.GetComponent<PlayerMovement>().checkSwitchBoxMove("betweenRooms", false);
        }
    }
}
