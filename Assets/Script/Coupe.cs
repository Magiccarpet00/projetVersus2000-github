using UnityEngine;

public class Coupe : MonoBehaviour
{
    public GameObject player1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.gameFinnished == false)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.gameObject == GameManager.instance.players[0])
                {
                    GameManager.instance.bandeauJ1Win.SetActive(true);
                    GameManager.instance.gameFinnished = true;
                }

                if (collision.gameObject == GameManager.instance.players[1])
                {
                    GameManager.instance.bandeauJ2Win.SetActive(true);
                    GameManager.instance.gameFinnished = true;
                }

                AudioManager.instance.PlayClipAt(GameManager.instance.sound_victoire, transform.position);


                StartCoroutine(GameManager.instance.WaitAndReload(2.5f));
            }
        }
    }
}
