using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Room roomOrigine;
    public Room roomDestination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
                if (GameManager.instance.playersPosition[collision.gameObject].GetComponent<Room>() == roomOrigine)
                {
                    //Debug.Log("Je viens de " + roomOrigine + " et vais vers " + roomDestination);

                    collision.GetComponent<PlayerMovement>().TapisRoulant(roomOrigine, roomDestination);

                    if(roomOrigine.roomFinnished == true && roomDestination.roomFinnished == false)
                    {
                    StartCoroutine(AttackVersusRetard(collision.gameObject));
                    }   

                }

                else if (GameManager.instance.playersPosition[collision.gameObject].GetComponent<Room>() == roomDestination)
                {
                    //Debug.Log("Je viens de " + roomDestination + " et vais vers " + roomOrigine);

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
            collision.GetComponent<PlayerMovement>().destinationSlide = collision.transform.position;
        }        
    }

    public IEnumerator AttackVersusRetard(GameObject player)
    {
        yield return new WaitForSeconds(1.2f); // On attend que le joueur rentre dans la salle
        player.GetComponent<PlayerCharacter>().goAttackVersus = 1;
    }

   

}
