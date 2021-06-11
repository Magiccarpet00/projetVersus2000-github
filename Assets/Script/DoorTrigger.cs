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
                    CheckGoAttackVerus(roomOrigine, roomDestination, collision.GetComponent<PlayerCharacter>());

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

    public void CheckGoAttackVerus(Room roomOrigine, Room roomDestination, PlayerCharacter player)
    {
        if (roomOrigine.roomFinnished && roomDestination.roomFinnished!)
        {
            player.goAttackVersus = 1;
            
        }
    }

}
