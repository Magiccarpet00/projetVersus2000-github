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
                  // Deplacement automatique de RoomOrigine vers RoomDestination
                  collision.GetComponent<PlayerMovement>().AutoWalk(roomOrigine, roomDestination);

                  if(roomOrigine.typeRoom == TypeRoom.VANILLA && roomDestination.typeRoom == TypeRoom.SHOP)
                  {
                       Animator animator = roomDestination.GetComponentInChildren<Animator>();
                       animator.SetTrigger("fadeoff");
                  }
                  if(roomOrigine.typeRoom == TypeRoom.SHOP && roomDestination.typeRoom == TypeRoom.VANILLA)
                  {
                       Animator animator = roomDestination.GetComponentInChildren<Animator>();
                       animator.SetTrigger("fadein");
                  }

              }

              else if (GameManager.instance.playersPosition[collision.gameObject].GetComponent<Room>() == roomDestination)
              {
                  // Deplacement automatique de RoomDestination vers RoomOrigine
                  collision.GetComponent<PlayerMovement>().AutoWalk(roomDestination, roomOrigine);


                  if (roomOrigine.typeRoom == TypeRoom.SHOP && roomDestination.typeRoom == TypeRoom.VANILLA)
                  {
                      Animator animator = roomDestination.GetComponentInChildren<Animator>();
                      animator.SetTrigger("fadeoff");
                  }
                  if (roomOrigine.typeRoom == TypeRoom.VANILLA && roomDestination.typeRoom == TypeRoom.SHOP)
                  {
                      Animator animator = roomDestination.GetComponentInChildren<Animator>();
                      animator.SetTrigger("fadein");
                  }
              }
         }
    }

    //[REFACTOT] J'ai enlever ce qu'il y avais dans OntriggerEnter2D de Room pour le mettre dans cette classe.
    //           Mais je dupplique du code avec les if roomDestination et roomOrigine
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            // Stop de l'autoWalk
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            playerMovement.isBetweenRooms = false;
            playerMovement.checkSwitchBoxMove("betweenRooms", false);
            playerMovement.destinationSlide = collision.transform.position;


            if (GameManager.instance.playersPosition[collision.gameObject].GetComponent<Room>() == roomOrigine)
            {     
                
                GameManager.instance.playersPosition[collision.gameObject] = roomDestination.gameObject;

                // Lancement de AttackVersus dans la prochaine room
                if (roomOrigine.roomFinnished == true && roomDestination.roomFinnished == false)
                {
                    StartCoroutine(AttackVersusRetard(collision.gameObject));
                }

                // Si on revient dans une room qu'on à fini on reste enfermé dedant sinon
                if (roomDestination.roomFinnished == false)
                {
                    roomDestination.CloseDoor();
                }

                if (roomDestination.typeRoom == TypeRoom.VANILLA && roomDestination.roomFinnished == false)
                {
                    roomDestination.patternInThisRoom.GetComponent<PatternEnemy>().ActivationEnnemy();
                }
            }
            else if (GameManager.instance.playersPosition[collision.gameObject].GetComponent<Room>() == roomDestination)
            {

                GameManager.instance.playersPosition[collision.gameObject] = roomOrigine.gameObject;

                // Lancement de AttackVersus dans la prochaine room
                if (roomDestination.roomFinnished == true && roomOrigine.roomFinnished == false)
                {
                    StartCoroutine(AttackVersusRetard(collision.gameObject));
                }

                // Si on revient dans une room qu'on à fini on reste enfermé dedant sinon
                if (roomOrigine.roomFinnished == false)
                {
                    roomOrigine.CloseDoor();
                }

                if (roomOrigine.typeRoom == TypeRoom.VANILLA && roomOrigine.roomFinnished == false)
                {
                    roomOrigine.patternInThisRoom.GetComponent<PatternEnemy>().ActivationEnnemy();
                }

            }

            if (collision.CompareTag("Player"))
            {
                ComboManager.instance.BankCombo(collision.gameObject);
            }

        }        
    }

    public IEnumerator AttackVersusRetard(GameObject player)
    {
        yield return new WaitForSeconds(1.2f); // On attend que le joueur rentre dans la salle
        player.GetComponent<PlayerCharacter>().goAttackVersus = 1;
    }

   

}
